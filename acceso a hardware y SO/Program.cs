using System;
using System.Management;
using System.Net.NetworkInformation;
using Microsoft.Win32;
using System.Diagnostics;

namespace acceso_a_hardware_y_SO
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ObtenerNumeroDeSerieDisco();
            ObtenerCantidadDeDiscos();
            ObtenerInventarioGeneral();
            ObtenerMacAddress();
            AccesoRegistroEjemplo();
            ObtenerProcesosActivos();
            Console.ReadLine();
        }

        static void ObtenerNumeroDeSerieDisco()
        {
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");

                foreach (ManagementObject wmi_HD in searcher.Get())
                {
                    Console.WriteLine("Modelo de Disco: " + wmi_HD["Model"]);
                    Console.WriteLine("Número de Serie: " + wmi_HD["SerialNumber"]);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el número de serie: " + ex.Message);
            }
        }

        static void ObtenerCantidadDeDiscos()
        {
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");
                int count = 0;

                foreach (ManagementObject wmi_HD in searcher.Get())
                {
                    count++;
                }

                Console.WriteLine("Cantidad de unidades de disco: " + count);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener la cantidad de discos: " + ex.Message);
            }
        }

        static void ObtenerInventarioGeneral()
        {
            try
            {
                ManagementObjectSearcher cpuSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");
                foreach (ManagementObject cpu in cpuSearcher.Get())
                {
                    Console.WriteLine("Procesador: " + cpu["Name"]);
                }

                ManagementObjectSearcher ramSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMemory");
                foreach (ManagementObject ram in ramSearcher.Get())
                {
                    Console.WriteLine("RAM: " + ram["Capacity"] + " bytes");
                }

                ManagementObjectSearcher nicSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapter");
                foreach (ManagementObject nic in nicSearcher.Get())
                {
                    Console.WriteLine("NIC: " + nic["Name"]);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el inventario: " + ex.Message);
            }
        }

        static void ObtenerMacAddress()
        {
            try
            {
                var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
                foreach (var ni in networkInterfaces)
                {
                    Console.WriteLine("Nombre: " + ni.Name);
                    Console.WriteLine("MAC Address: " + ni.GetPhysicalAddress());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener la MAC Address: " + ex.Message);
            }
        }

        static void AccesoRegistroEjemplo()
        {
            try
            {
                string clave = @"SOFTWARE\MiAplicacion";
                RegistryKey registryKey = Registry.CurrentUser.CreateSubKey(clave);
                registryKey.SetValue("MiValor", "ValorEjemplo");
                Console.WriteLine("Clave creada y valor establecido.");

                Console.WriteLine("Valor leído: " + registryKey.GetValue("MiValor"));

                registryKey.SetValue("MiValor", "NuevoValor");
                Console.WriteLine("Valor modificado: " + registryKey.GetValue("MiValor"));

                registryKey.DeleteValue("MiValor");
                Console.WriteLine("Valor borrado.");

                registryKey.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al acceder al registro: " + ex.Message);
            }
        }

        static void ObtenerProcesosActivos()
        {
            try
            {
                Process[] procesos = Process.GetProcesses();
                Console.WriteLine("Procesos activos:");
                foreach (Process p in procesos)
                {
                    Console.WriteLine($"ID: {p.Id}, Nombre: {p.ProcessName}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener procesos activos: " + ex.Message);
            }
        }
    }
}
