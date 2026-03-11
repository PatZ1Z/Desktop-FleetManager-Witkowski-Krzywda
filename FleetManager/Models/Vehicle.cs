namespace FleetManager.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

public class Vehicle : ReactiveObject
{
    [Reactive] public int VehicleId { get; set; }
    [Reactive] public string VehicleName { get; set; } = string.Empty;
    [Reactive] public string VehicleTag { get; set; } = string.Empty;
    [Reactive] public int VehicleFuel { get; set; }
    [Reactive] public string VehicleStatus { get; set; } = string.Empty;
}