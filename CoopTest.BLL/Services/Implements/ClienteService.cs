using AutoMapper;
using CoopTest.BLL.DTOs;
using CoopTest.BLL.Repository;
using CoopTest.DAL.Models;

namespace CoopTest.BLL.Services.Implements
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IFondoRepository _fondoRepository;
        private readonly ITransaccionRepository _transaccionRepository;
        private readonly IMapper _mapper;

        private const decimal MontoInicial = 500000m; // Monto inicial en COP
        private const string TipoSuscripcion = "Suscripción";
        private const string TipoCancelacion = "Cancelación";

        public ClienteService(
            IClienteRepository clienteRepository,
            IFondoRepository fondoRepository,
            ITransaccionRepository transaccionRepository,
            IMapper mapper)
        {
            _clienteRepository = clienteRepository;
            _fondoRepository = fondoRepository;
            _transaccionRepository = transaccionRepository;
            _mapper = mapper;
        }

        public async Task CrearOActualizarClienteAsync(ClienteDTO clienteDTO)
        {
            // Verificar si el cliente ya existe
            var clienteExistente = await _clienteRepository.GetClienteWithFondosAsync(clienteDTO.Id);

            if (clienteExistente == null)
            {
                // Crear un nuevo cliente solo si no existe un cliente con el mismo ID
                var cliente = _mapper.Map<Cliente>(clienteDTO);
                cliente.Fondos = new List<FondoVinculado>(); // Inicializar la lista de fondos

                // Insertar el cliente en la base de datos
                await _clienteRepository.InsertAsync(cliente);

                // Log para verificar la creación
                Console.WriteLine($"Cliente creado: {cliente.Id}, Saldo: {cliente.Saldo}");
            }
            else
            {
                // Si el cliente ya existe, actualiza el nombre y el saldo
                clienteExistente.Nombre = clienteDTO.Nombre;

                // Asegurarse de que el saldo recibido es válido
                if (clienteDTO.Saldo >= 0)
                {
                    clienteExistente.Saldo = clienteDTO.Saldo; // Actualizar el saldo del cliente
                }
                else
                {
                    throw new ArgumentException("El saldo debe ser un valor no negativo.");
                }

                // Actualizar el cliente en la base de datos
                await _clienteRepository.UpdateAsync(clienteExistente.Id, clienteExistente);

                // Log para verificar la actualización
                Console.WriteLine($"Cliente actualizado: {clienteExistente.Id}, Saldo: {clienteExistente.Saldo}");

                // Prueba: Verificar el estado del cliente después de la actualización
                var clienteGuardado = await _clienteRepository.GetClienteWithFondosAsync(clienteDTO.Id);
                Console.WriteLine($"Cliente actualizado: {clienteGuardado?.Id}, Saldo: {clienteGuardado?.Saldo}");
            }
        }

        public async Task SuscribirClienteAFondoAsync(SuscripcionFondoDTO suscripcionFondoDTO)
        {
            // Obtener el cliente con sus fondos vinculados
            var cliente = await _clienteRepository.GetClienteWithFondosAsync(suscripcionFondoDTO.ClienteId);

            if (cliente == null)
                throw new Exception("Cliente no encontrado");

            // Obtener el fondo al que se va a suscribir el cliente
            var fondo = await _fondoRepository.GetByIdAsync(suscripcionFondoDTO.FondoId);

            if (fondo == null)
                throw new Exception("Fondo no encontrado");

            // Verificar si el cliente tiene suficiente saldo para la suscripción
            if (cliente.Saldo < fondo.MontoMinimo)
                throw new Exception($"No tiene saldo suficiente para vincularse al fondo {fondo.Nombre}");

            // Descontar el monto de la suscripción del saldo del cliente
            cliente.Saldo -= fondo.MontoMinimo;

            // Crear un objeto FondoVinculado a partir del fondo y agregarlo a la lista de fondos del cliente
            var fondoVinculado = _mapper.Map<FondoVinculado>(fondo);
            fondoVinculado.FechaVinculacion = DateTime.Now;
            fondoVinculado.IdFondo = fondo.Id; // Asignar el ID del fondo manualmente

            cliente.Fondos.Add(fondoVinculado);

            // Actualizar el cliente con la suscripción en la base de datos
            await _clienteRepository.UpdateAsync(cliente.Id, cliente);

            // Crear y registrar la transacción de suscripción
            var transaccion = _mapper.Map<Transaccion>(suscripcionFondoDTO);
            transaccion.Id = Guid.NewGuid().ToString(); // Generar el identificador único aquí
            transaccion.TipoTransaccion = TipoSuscripcion;
            transaccion.FechaTransaccion = DateTime.Now;
            transaccion.Monto = fondo.MontoMinimo;
            transaccion.IdCliente = cliente.Id;
            transaccion.IdFondo = fondo.Id;

            await _transaccionRepository.InsertAsync(transaccion);
        }



        public async Task CancelarSuscripcionAsync(string clienteId, string fondoId)
        {
            var cliente = await _clienteRepository.GetClienteWithFondosAsync(clienteId);

            if (cliente == null)
                throw new Exception("Cliente no encontrado");

            var fondoVinculado = cliente.Fondos.FirstOrDefault(f => f.IdFondo == fondoId);

            if (fondoVinculado == null)
                throw new Exception("El cliente no está vinculado a este fondo");

            // Retornar el monto al saldo del cliente
            cliente.Saldo += fondoVinculado.MontoMinimo;
            cliente.Fondos.Remove(fondoVinculado);

            // Actualizar cliente en la base de datos
            await _clienteRepository.UpdateAsync(clienteId, cliente);

            // Usar AutoMapper para mapear de FondoVinculado a Transaccion
            var transaccion = _mapper.Map<Transaccion>(fondoVinculado);
            transaccion.IdCliente = clienteId; // Asignar el Id del cliente
            transaccion.IdFondo = fondoId; // Asignar el Id del fondo

            await _transaccionRepository.InsertAsync(transaccion);
        }



        public async Task<IEnumerable<Transaccion>> VerHistorialTransaccionesAsync(string clienteId)
        {
            return await _transaccionRepository.GetTransaccionesPorClienteAsync(clienteId);
        }
    }
}
