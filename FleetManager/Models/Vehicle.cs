using System;
using System.Collections.Generic;
using Avalonia.Media;

namespace FleetManager.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

public class Vehicle : ReactiveObject
{
    [Reactive] public int VehicleId { get; set; }

    [Reactive] public string VehicleName { get; set; } = string.Empty;

    [Reactive] public string VehicleTag { get; set; } = string.Empty;

    [Reactive] public double VehicleFuel { get; set; }

    [Reactive] public string VehicleStatus { get; set; } = "Available";
    
    public bool CanChangeStatus => VehicleFuel > 0.14 && VehicleStatus != "Service";
    
    public Vehicle()
    {
        
        this.WhenAnyValue(x => x.VehicleFuel, x => x.VehicleStatus)
            .Subscribe(_ =>
            {
                this.RaisePropertyChanged(nameof(CanChangeStatus));
                this.RaisePropertyChanged(nameof(StatusColor));
                
            });
    }
    
    public IBrush StatusColor =>
        VehicleStatus switch
        {
            "Available" => Brushes.Green,
            "In Route" => Brushes.Orange,
            "Service" => Brushes.Red,
            _ => Brushes.Black
        };
}