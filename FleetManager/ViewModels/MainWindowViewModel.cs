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
    public ObservableCollection<Vehicle> Vehicles { get; set; } = [];
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
            File.WriteAllText(FilePath, JsonSerializer.Serialize(Vehicles, _options));
            Console.WriteLine("Vehicles saved to {0}", FilePath);
        }
        catch(Exception e) when(
            e is IOException or
                UnauthorizedAccessException or
                JsonException 
        )
        {
            Console.WriteLine($"Save exception: {e.Message}");
        }
        catch (Exception e)
        {
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
                Vehicles.Add(vehicle);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}