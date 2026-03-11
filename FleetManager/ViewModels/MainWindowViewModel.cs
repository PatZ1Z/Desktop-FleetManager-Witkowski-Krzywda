using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using FleetManager.Models;

namespace FleetManager.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public ObservableCollection<Vehicle> Vechicles { get; set; } = [];
    
    private const string FilePath = "Data/vechicles.json";

    public MainWindowViewModel()
    {
        
    }
    
    private void LoadCharacters()
    {
        if(!File.Exists(FilePath)) return;
        try
        {
            var jsonData = File.ReadAllText(FilePath);
            var list = JsonSerializer.Deserialize<List<Vehicle>>(jsonData);
            Vechicles.Clear();
            if (list == null) return;
            foreach (var vechicle in list)
            {
                Vechicles.Add(vechicle);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        
    }
}