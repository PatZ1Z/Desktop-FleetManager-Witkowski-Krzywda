using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using FleetManager.Models;

namespace FleetManager.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public ObservableCollection<Vehicle> Vehicles { get; set; } = [];
    
    private const string FilePath = "Data/vehicles.json";

    public MainWindowViewModel()
    {
        LoadCharacters();
    }
    
    private void LoadCharacters()
    {
        if (!File.Exists(FilePath))
        {
            Console.WriteLine($"File {FilePath} not found");
            return;
        }
        try
        {
            var jsonData = File.ReadAllText(FilePath);
            var list = JsonSerializer.Deserialize<List<Vehicle>>(jsonData);
            Vehicles.Clear();
            if (list == null) return;
            foreach (var vechicle in list)
            {
                Vehicles.Add(vechicle);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        
    }
}