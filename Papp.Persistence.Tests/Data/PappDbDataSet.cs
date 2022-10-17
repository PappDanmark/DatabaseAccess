using Papp.Domain;

namespace Papp.Persistence.Tests.Data;

public static class PappDbDataSet
{

    public static Booth[] Booths = new Booth[] {
        new() {
            Id = new("dcd6a274-b7fb-41aa-b099-020296b70e5a"),
            BoothNumber = 1,
            MuncipalityId = "m2"
        },
        new() {
            Id = new("029d6427-adf2-4746-a33f-cfc60a51e4e2"),
            BoothNumber = 2,
            MuncipalityId = "m2"
        },
        new() {
            Id = new("cab12fe3-a366-4602-bafa-8a92a9cc53f9"),
            BoothNumber = 3,
            MuncipalityId = "m3"
        }
    };



}