using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;


public class Cerial
{

    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; init; }
    [NotNull, Column("cerial_name")]
    public string Name { get; init; }
    [NotNull]
    public string mfr { get; init; }
    [NotNull, Column("cerial_type")]
    public string Type { get; init; }
    public int Calories { get; init; }
    public int Protein { get; init; }
    public int Fat { get; init; }
    public int Sodium { get; init; }
    public float Fiber { get; init; }
    public float Carbo { get; init; }
    public int Sugars { get; init; }
    public int Potass { get; init; }
    public int Vitamins { get; init; }
    public int Shelf { get; init; }
    public float Weight { get; init; }
    public float Cups { get; init; }
    public float Rating { get; init; }

    private static Dictionary<string, string> mfrMap = new(){
    {"A", "American Home Food Product"},
    {"G", "General Mills"},
    {"K","Kelloggs"},
    {"N","Nabisco"},
    {"P","Post"},
    {"Q","Quater Oats"},
    {"R","Ralston Puria"},
};

    public Cerial(string name, string mfr, string type, int calories, int protein, int fat, int sodium, float fiber, float carbo, int sugars, int potass, int vitamins, int shelf, float weight, float cups, float rating)
    {
        Name = name;
        this.mfr = mfr;
        Type = type;
        Calories = calories;
        Protein = protein;
        Fat = fat;
        Sodium = sodium;
        Fiber = fiber;
        Carbo = carbo;
        Sugars = sugars;
        Potass = potass;
        Vitamins = vitamins;
        Shelf = shelf;
        Weight = weight;
        Cups = cups;
        Rating = rating;
    }

    public string GetMfr()
    {
        return mfrMap[mfr];
    }
    
    // override object.Equals
    public override bool Equals(object obj)
    {
        //
        // See the full list of guidelines at
        //   http://go.microsoft.com/fwlink/?LinkID=85237
        // and also the guidance for operator== at
        //   http://go.microsoft.com/fwlink/?LinkId=85238
        //

        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }
        Cerial other = (Cerial)obj;
        // TODO: write your implementation of Equals() here
        var equals = this.Name == other.Name
            && this.mfr == other.mfr
            && this.Type == other.Type
            && this.Calories == other.Calories
            && this.Protein == other.Protein
            && this.Fat == other.Fat
            && this.Sodium == other.Sodium
            && this.Fiber == other.Fiber
            && this.Carbo == other.Carbo
            && this.Sugars == other.Sugars
            && this.Potass == other.Potass
            && this.Vitamins == other.Vitamins
            && this.Shelf == other.Shelf
            && this.Weight == other.Weight
            && this.Cups == other.Cups
            && this.Rating == other.Rating;
        return equals;
    }

}