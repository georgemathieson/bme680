using System;
using System.Device.I2c;
using System.Device.I2c.Drivers;
using System.Threading;

namespace Bme680.Samples
{
    /// <summary>
    /// Sample program for reading <see cref="Bme680"/> sensor data on a Raspberry Pi.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Main entry point for the program.
        /// </summary>
        static void Main()
        {
            Console.WriteLine("Hello BME680!");

            // The I2C bus ID on the Raspberry Pi 3.
            const int busId = 1;

            var i2cConnectionSettings = new I2cConnectionSettings(busId, Bme680.DefaultI2cAddress);
            var unixI2cDevice = new UnixI2cDevice(i2cConnectionSettings);

            using (var bme680 = new Bme680(unixI2cDevice))
            {
                bme680.SetTemperatureOversampling(Oversampling.x2);

                while (true)
                {
                    // Once a reading has been taken, the sensor goes back to sleep mode.
                    if (bme680.GetPowerMode() == PowerMode.Sleep)
                    {
                        // This instructs the sensor to take a measurement.
                        bme680.SetPowerMode(PowerMode.Forced);
                    }

                    // This prevent us from reading old data from the sensor.
                    if (bme680.HasNewData())
                    {
                        var temperature = bme680.ReadTemperature();

                        Console.WriteLine($"{Math.Round(temperature.Celsius, 2).ToString("N2")}°c");

                        Thread.Sleep(100);
                    }
                }
            }
        }
    }
}
