using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reactive;
using System.Text.Json;
using System.Text.Json.Serialization;
using FleetManager.Models;
using ReactiveUI;

namespace FleetManager.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public ObservableCollection<VehicleItemViewModel> Vehicles { get; set; } = [];
    private static readonly JsonSerializerOptions _options = new(){WriteIndented=true};
    
    private const string FilePath = "Data/vehicles.json";
    
    public ReactiveCommand<Unit, Unit> SaveCommand { get; }
    
    

    public MainWindowViewModel()
    {
        LoadVehicles();
        
        SaveCommand = ReactiveCommand.Create(SaveToJson);
    }
    
    
    
    private void SaveToJson()
    {
        try
        {
            var models = new List<Vehicle>();

            Console.WriteLine("=== Saving Vehicles ===");

            foreach (var vm in Vehicles)
            {
                var vehicle = vm.GetModel();

                // 🔹 Debug: pokazujemy wszystkie kluczowe właściwości
                Console.WriteLine($"VehicleId={vehicle.VehicleId}, Name={vehicle.VehicleName}, Tag={vehicle.VehicleTag}, Fuel={vehicle.VehicleFuel}, Status={vehicle.VehicleStatus}");

                models.Add(vehicle);
            }

            File.WriteAllText(FilePath, JsonSerializer.Serialize(models, _options));

            Console.WriteLine($"Vehicles saved to {FilePath}");
            Console.WriteLine("=======================");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Save exception: {e.Message}");
            Console.WriteLine(e);
        }
    }
    
    private void LoadVehicles()
    {
        if (!File.Exists(FilePath))
        {
            Console.WriteLine($"File {FilePath} not found");
            return;
        }

        try
        {
            var jsonData = File.ReadAllText(FilePath);
            
            var list = JsonSerializer.Deserialize<List<Vehicle>>(jsonData, _options);

            Vehicles.Clear();
            if (list == null) return;

            foreach (var vehicle in list)
            {
                Vehicles.Add(new VehicleItemViewModel(vehicle));
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}