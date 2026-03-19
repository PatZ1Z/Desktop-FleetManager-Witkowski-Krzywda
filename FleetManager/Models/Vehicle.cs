using System;
using System.Collections.Generic;
using System.Reactive;
using Avalonia.Media;

namespace FleetManager.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

public class Vehicle
{
    public int VehicleId { get; set; }

    public string VehicleName { get; set; } = string.Empty;

    public string VehicleTag { get; set; } = string.Empty;

    public double VehicleFuel { get; set; }

    public string VehicleStatus { get; set; } = "Available";

    public bool CanChangeStatus =>
        VehicleFuel > 0.14 && VehicleStatus != "Service";

    public void AddFuel(double amount)
    {
        VehicleFuel += amount;
    }
}