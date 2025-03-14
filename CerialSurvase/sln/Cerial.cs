using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

public class Cerial
{

    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int? ID { get; init; }
    [NotNull, Column("cerial_name")]
    public string Name { get; private set; }
    [NotNull]
    public string mfr { get; private set; }
    [NotNull, Column("cerial_type")]
    public string Type { get; private set; }
    public int Calories { get; private set; }
    public int Protein { get; private set; }
    public int Fat { get; private set; }
    public int Sodium { get; private set; }
    public float Fiber { get; private set; }
    public float Carbo { get; private set; }
    public int Sugars { get; private set; }
    public int Potass { get; private set; }
    public int Vitamins { get; private set; }
    public int Shelf { get; private set; }
    public float Weight { get; private set; }
    public float Cups { get; private set; }
    public float Rating { get; private set; }

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

    public Cerial Update(Cerial updatedCerial)
    {
        this.Name = updatedCerial.Name;
        this.mfr = updatedCerial.mfr;
        this.Type = updatedCerial.Type;
        this.Calories = updatedCerial.Calories;
        this.Protein = updatedCerial.Protein;
        this.Fat = updatedCerial.Fat;
        this.Sodium = updatedCerial.Sodium;
        this.Fiber = updatedCerial.Fiber;
        this.Carbo = updatedCerial.Carbo;
        this.Sugars = updatedCerial.Sugars;
        this.Potass = updatedCerial.Potass;
        this.Vitamins = updatedCerial.Vitamins;
        this.Shelf = updatedCerial.Shelf;
        this.Weight = updatedCerial.Weight;
        this.Cups = updatedCerial.Cups;
        this.Rating = updatedCerial.Rating;
        return this;
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
        var equals = this.ID == other.ID &&
            this.Name == other.Name;
        return equals;
    }

}