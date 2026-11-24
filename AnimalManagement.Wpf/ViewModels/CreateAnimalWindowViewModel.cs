using System.Collections.ObjectModel;
using System.Globalization;
using AnimalManagement.Wpf.Infrastructure.Translations;
using AnimalManagement.Wpf.Models.Requests;
using AnimalManagement.Wpf.Models.Responses;
using AnimalManagement.Wpf.Services.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AnimalManagement.Wpf.ViewModels;

public partial class CreateAnimalWindowViewModel : BaseViewModel
{
    // Dependencies
    readonly TranslationStore translationStore;
    readonly IAnimalService animalService;

    // Observable properties
    [ObservableProperty]
    private string? name;
    [ObservableProperty]
    private string? age;
    [ObservableProperty]
    private string? weight;
    [ObservableProperty] 
    private ObservableCollection<EnumTranslationResponse> colors = [];
    [ObservableProperty]
    private EnumTranslationResponse? selectedColor;
    [ObservableProperty] 
    private ObservableCollection<EnumTranslationResponse> genders = [];
    [ObservableProperty]
    private EnumTranslationResponse? selectedGender;

    // Other observable properties
    [ObservableProperty] 
    private bool canBeCreated;

    public CreateAnimalWindowViewModel(
        TranslationStore translationStore,
        IAnimalService animalService)
    {
        this.translationStore = translationStore;
        this.animalService = animalService;
        Console.WriteLine($"Colors: {translationStore.Colors.Count}");
        Console.WriteLine($"Genders: {translationStore.Genders.Count}");
        Initialize();
        translationStore.TranslationsChanged += (_, _) =>  InitializeCollections();
    }

    void Initialize()
    {
        CanBeCreated = true;
        InitializeCollections();
    }

    void InitializeCollections()
    {
        int selectedColorCode = SelectedColor?.Code ?? 0;
        int selectedGenderCode = SelectedColor?.Code ?? 0;
        Colors.Clear();
        translationStore.Colors.ForEach(x => Colors.Add(x));
        Genders.Clear();
        translationStore.Genders.ForEach(x => Genders.Add(x));
        
        SelectedColor = selectedColorCode == 0 ? Colors.FirstOrDefault() : Colors.SingleOrDefault(c => c.Code == selectedColorCode);
        SelectedGender = selectedGenderCode == 0 ? Genders.FirstOrDefault() : Genders.SingleOrDefault(c => c.Code == selectedGenderCode);
    }

    // Commands
    [RelayCommand]
    public async Task CreateAnimalAsync()
    {
        try
        {
            CanBeCreated = false;

            if(!int.TryParse(Age, out var parsedAge))
            {
                Console.WriteLine("Invalid age");
                Age = string.Empty;
            }
            if(!double.TryParse(Weight, CultureInfo.InvariantCulture, out var parsedWeight))
            {
                Console.WriteLine("Invalid weight");
                Weight = string.Empty;
            }

            Console.WriteLine($"{Name} {Age} {Weight} {SelectedGender?.Translation} {SelectedColor?.Translation}");

            var request = new CreateAnimalRequest(Name ?? "", parsedAge, parsedWeight, SelectedGender?.Code ?? 0, SelectedColor?.Code ?? 0);
            
            var result = await animalService.CreateAsync(request);

            if (result is null)
            {
                Console.WriteLine("Failed to create animal");
                return;
            }

            Console.WriteLine($"Successfully created animal: {result.Name}");
        }
        finally
        {
            CanBeCreated = true;
        }
    }
}