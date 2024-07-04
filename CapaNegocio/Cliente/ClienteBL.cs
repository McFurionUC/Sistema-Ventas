using CapaBaseDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaNegocio;

namespace CapaNegocio
{ 
    public class ClienteBL
    {
    VentasDataContext ventas = new VentasDataContext();

    public List<Cliente> Listar()
    {
            var listar = from C in ventas.Cliente
                         select C;
            return listar.ToList();
    }

            public string Agregar(string CodCliente, string Apellido,string  Nombre, string Direccion)
            {
                Cliente clienteAgregar = new Cliente();
                
                clienteAgregar.Apellidos = Apellido;
                clienteAgregar.Nombres = Nombre;
                clienteAgregar.CodCliente = CodCliente;
                clienteAgregar.Direccion = Direccion;
                ventas.Cliente.InsertOnSubmit(clienteAgregar);
                try
                {
                    ventas.SubmitChanges(); // Confirma la transacción
                    return "Agregado exitoso";
                }
                catch (Exception ex)
                {

                    return ex.Message;
                }
            }
        public  string Eliminar(string CodCliente)
        {
            var CodClienteEliminar = (from C in ventas.Cliente
                                      where C.CodCliente.Contains(CodCliente)
                                      select C).First();

            ventas.Cliente.DeleteOnSubmit(CodClienteEliminar);
            try
            {
                ventas.SubmitChanges(); // Confirma la eliminación
                return "Eliminacion Correcta";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string Actualizar(string CodCliente, string Apellido, string Nombre, string Direccion)
        {
            var clienteActualizar = (from C in ventas.Cliente
                                     where C.CodCliente.Contains(CodCliente)
                                     select C).First();

            if (clienteActualizar != null)
            {
                clienteActualizar.Apellidos = Apellido;
                clienteActualizar.Nombres = Nombre;
                clienteActualizar.Direccion = Direccion;

                try
                {
                    ventas.SubmitChanges(); // Confirma la transacción
                    return "Actualización exitosa";
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
            else
            {
                return "Cliente no encontrado";
            }

        }

    }
}
