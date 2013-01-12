using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ailon.WP.Utils
{
    public static class PhoneNameResolver
    {
        public static CanonicalPhoneName Resolve(string manufacturer, string model)
        {
            var manufacturerNormalized = manufacturer.Trim().ToUpper();

            switch (manufacturerNormalized)
            {
                case "NOKIA":
                    return ResolveNokia(manufacturer, model);
                default:
                    return new CanonicalPhoneName()
                    {
                        ReportedManufacturer = manufacturer,
                        ReportedModel = model,
                        CanonicalManufacturer = manufacturer,
                        CanonicalModel = model,
                        IsResolved = false
                    };

            }
        }

        private static CanonicalPhoneName ResolveNokia(string manufacturer, string model)
        {
            var modelNormalized = model.Trim().ToUpper();

            var result = new CanonicalPhoneName()
            {
                ReportedManufacturer = manufacturer,
                ReportedModel = model,
                CanonicalManufacturer = "NOKIA",
                CanonicalModel = model,
                IsResolved = false
            };

            var lookupValue = modelNormalized;
            if (modelNormalized.StartsWith("RM-"))
            {
                lookupValue = modelNormalized.Substring(0, 6);
            }

            if (nokiaLookupTable.ContainsKey(lookupValue))
            {
                var modelMetadata = nokiaLookupTable[lookupValue];
                if (nokiaCanonicalModels.ContainsKey(modelMetadata.ModelId))
                {
                    result.CanonicalModel = nokiaCanonicalModels[modelMetadata.ModelId].CanonicalModel;
                    result.Comments = modelMetadata.Description;
                    result.IsResolved = true;
                }
            }

            return result;
        }

        private static Dictionary<string, CanonicalPhoneName> nokiaCanonicalModels = new Dictionary<string, CanonicalPhoneName>()
        {
            { "Lumia505", new CanonicalPhoneName() { CanonicalModel = "Lumia 505" } }, 
            { "Lumia510", new CanonicalPhoneName() { CanonicalModel = "Lumia 510" } }, 
            { "Lumia610", new CanonicalPhoneName() { CanonicalModel = "Lumia 610" } }, 
            { "Lumia620", new CanonicalPhoneName() { CanonicalModel = "Lumia 620" } }, 
            { "Lumia710", new CanonicalPhoneName() { CanonicalModel = "Lumia 710" } }, 
            { "Lumia800", new CanonicalPhoneName() { CanonicalModel = "Lumia 800" } }, 
            { "Lumia810", new CanonicalPhoneName() { CanonicalModel = "Lumia 810" } }, 
            { "Lumia820", new CanonicalPhoneName() { CanonicalModel = "Lumia 820" } }, 
            { "Lumia822", new CanonicalPhoneName() { CanonicalModel = "Lumia 822" } }, 
            { "Lumia900", new CanonicalPhoneName() { CanonicalModel = "Lumia 900" } }, 
            { "Lumia920", new CanonicalPhoneName() { CanonicalModel = "Lumia 920" } } 
        };

        private static Dictionary<string, ReportedNameMetadata> nokiaLookupTable = new Dictionary<string, ReportedNameMetadata>()
        {
            // Lumia 505
            { "LUMIA 505", new ReportedNameMetadata("Lumia505", null) },
            // Lumia 510
            { "LUMIA 510", new ReportedNameMetadata("Lumia510", null) },
            { "NOKIA 510", new ReportedNameMetadata("Lumia510", null) },
            // Lumia 610
            { "LUMIA 610", new ReportedNameMetadata("Lumia610", null) },
            { "LUMIA 610 NFC", new ReportedNameMetadata("Lumia610", "NFC") },
            { "NOKIA 610", new ReportedNameMetadata("Lumia610", null) },
            { "NOKIA 610C", new ReportedNameMetadata("Lumia610", null) },
            // Lumia 620
            { "LUMIA 620", new ReportedNameMetadata("Lumia620", null) },
            { "RM-846", new ReportedNameMetadata("Lumia620", null) },
            // Lumia 710
            { "LUMIA 710", new ReportedNameMetadata("Lumia710", null) },
            { "NOKIA 710", new ReportedNameMetadata("Lumia710", null) },
            // Lumia 800
            { "LUMIA 800", new ReportedNameMetadata("Lumia800", null) },
            { "NOKIA 800", new ReportedNameMetadata("Lumia800", null) },
            { "NOKIA 800C", new ReportedNameMetadata("Lumia800", "China") },
            // Lumia 810
            { "RM-878", new ReportedNameMetadata("Lumia810", "T-Mobile USA") },
            // Lumia 820
            { "RM-824", new ReportedNameMetadata("Lumia820", null) },
            { "RM-825", new ReportedNameMetadata("Lumia820", null) },
            { "RM-826", new ReportedNameMetadata("Lumia820", null) },
            // Lumia 822
            { "RM-845", new ReportedNameMetadata("Lumia822", "Verizon") },
            // Lumia 900
            { "LUMIA 900", new ReportedNameMetadata("Lumia900", null) },
            { "NOKIA 900", new ReportedNameMetadata("Lumia900", null) },
            // Lumia 920
            { "RM-820", new ReportedNameMetadata("Lumia920", null) },
            { "RM-821", new ReportedNameMetadata("Lumia920", null) },
            { "RM-822", new ReportedNameMetadata("Lumia920", "China") },
            { "RM-867", new ReportedNameMetadata("Lumia920", "920T") },
            { "NOKIA 920", new ReportedNameMetadata("Lumia920", null) },
        };
    }

    public class CanonicalPhoneName
    {
        public string ReportedManufacturer { get; set; }
        public string ReportedModel { get; set; }
        public string CanonicalManufacturer { get; set; }
        public string CanonicalModel { get; set; }
        public string Comments { get; set; }
        public bool IsResolved { get; set; }
    }

    class ReportedNameMetadata
    {
        internal string ModelId { get; set; }
        internal string Description { get; set; }

        internal ReportedNameMetadata(string modelId, string description)
        {
            this.ModelId = modelId;
            this.Description = description;
        }
    }
}
