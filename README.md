# BME680 Sensor

[![Build Status](https://travis-ci.org/georgemathieson/bme680.svg?branch=master)](https://travis-ci.org/georgemathieson/bme680)

## Summary
A C# .NET Core library for the BME680 gas, temperature, humidity and pressure sensor. 

[Datasheet](https://ae-bst.resource.bosch.com/media/_tech/media/datasheets/BST-BME680-DS001.pdf) for the BME680.

## Useage

Examples on how to use this device binding are available in the [samples](Bme680.Samples) folder.

### Running on the Raspberry Pi
See [.NET Core on Raspberry Pi](https://github.com/dotnet/core/blob/master/samples/RaspberryPiInstructions.md)

### Connection Type

The following connection types are supported by this binding.

- [X] I2C
- [ ] SPI

### Sensors

The following sensors are supported by this binding.

- [X] Temperature
- [X] Humidity
- [ ] Pressure
- [ ] Gas
