// (ViewModel dla UserControl)

using System;
using System.Reactive;
using Avalonia.Media;
using FleetManager.Models;
using ReactiveUI;

namespace FleetManager.ViewModels;

public class VehicleItemViewModel : ReactiveObject
{
    private readonly Vehicle _vehicle;
    
    public Vehicle GetModel() => _vehicle;

    public VehicleItemViewModel(Vehicle vehicle)
    {
        _vehicle = vehicle;
        var canFuel = this.WhenAnyValue(
            x => x.VehicleStatus,
            x => x.VehicleFuel,
            (status, fuel) => status != "In Route" && fuel < 1.0
        );

        AddFuelCommand = ReactiveCommand.Create(() =>
        {
            VehicleFuel = Math.Min(1.0, VehicleFuel + 0.15);
        }, canFuel);
        
        this.WhenAnyValue(x => x.VehicleFuel, x => x.VehicleStatus)
            .Subscribe(_ =>
            {
                this.RaisePropertyChanged(nameof(CanChangeStatus));
                this.RaisePropertyChanged(nameof(StatusColor));
            });
    }
    

    public int VehicleId => _vehicle.VehicleId;

    public string VehicleName => _vehicle.VehicleName;

    public string VehicleTag => _vehicle.VehicleTag;

    public double VehicleFuel
    {
        get => _vehicle.VehicleFuel;
        set
        {
            if (_vehicle.VehicleFuel == value)
                return;

            _vehicle.VehicleFuel = value;
            this.RaisePropertyChanged();
        }
    }

    public string VehicleStatus
    {
        get => _vehicle.VehicleStatus;
        set
        {
            if (_vehicle.VehicleStatus == value)
                return;

            _vehicle.VehicleStatus = value;
            this.RaisePropertyChanged();
        }
    }
    

    public bool CanChangeStatus => _vehicle.CanChangeStatus;

    public ReactiveCommand<Unit, Unit> AddFuelCommand { get; }

    public IBrush StatusColor =>
        VehicleStatus switch
        {
            "Available" => Brushes.Green,
            "In Route" => Brushes.Orange,
            "Service" => Brushes.Red,
            _ => Brushes.Black
        };
}