using System;
using System.Device.I2c;
using System.Device.I2c.Drivers;
using System.Threading;
using CommandLine;
using Newtonsoft.Json;

namespace Bme680.Samples
{
    /// <summary>
    /// Sample program for reading <see cref="Bme680"/> sensor data on a Raspberry Pi.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// CLI options the program was called with.
        /// </summary>
        private static Options _options;

        /// <summary>
        /// Main entry point for the program.
        /// </summary>
        static int Main(string[] args)
        {
            // Parse options passed from the command line.
            var parseResult = Parser.Default.ParseArguments<Options>(args).WithParsed(options => _options = options);
            if (parseResult is NotParsed<Options>)
            {
                // Invalid options passed to program, exit with non-ok status code.
                return -1;
            }

            if(_options.Quiet == false)
            {
                Console.WriteLine("Hello BME680!");
            }

            // The I2C bus ID on the Raspberry Pi 3.
            const int busId = 1;

            var i2cConnectionSettings = new I2cConnectionSettings(busId, Bme680.DefaultI2cAddress);
            var unixI2cDevice = new UnixI2cDevice(i2cConnectionSettings);

            using (var bme680 = new Bme680(unixI2cDevice))
            {
                bme680.Reset();
                bme680.SetHumidityOversampling(Oversampling.x1);
                bme680.SetTemperatureOversampling(Oversampling.x2);
                bme680.SetPressureOversampling(Oversampling.x16);

                while (true)
                {
                    // Once a reading has been taken, the sensor goes back to sleep mode.
                    if (bme680.PowerMode == PowerMode.Sleep)
                    {
                        // This instructs the sensor to take a measurement.
                        bme680.SetPowerMode(PowerMode.Forced);
                    }

                    // This prevent us from reading old data from the sensor.
                    if (bme680.HasNewData)
                    {
                        var reading = new
                        {
                            Temperature = Math.Round(bme680.Temperature.Celsius, 2).ToString("N2"),
                            Pressure = Math.Round(bme680.Pressure / 100, 2).ToString("N2"),
                            Humidity = Math.Round(bme680.Humidity, 2).ToString("N2")
                        };

                        if(_options.JsonOutput)
                        {
                            Console.WriteLine(JsonConvert.SerializeObject(reading));
                        }
                        else
                        {
                            Console.WriteLine($"{reading.Temperature} °c | {reading.Pressure} hPa | {reading.Humidity} %rH");
                        }

                        Thread.Sleep(1000);
                    }
                }
            }
        }
    }
}
