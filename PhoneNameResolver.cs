/*
 * Copyright (c) 2013 by Alan Mendelevich
 * 
 * Licensed under MIT license.
 * 
 * See license.txt for details.
*/

using System.Collections.Generic;

namespace Ailon.WP.Utils
{
    public enum ScreenResolution
    {
        WVGA,
        WXGA,
        _720p
    }

    public static class PhoneNameResolver
    {
        public static CanonicalPhoneName Resolve(string manufacturer, string model)
        {
            var manufacturerNormalized = manufacturer.Trim().ToUpper();

            switch (manufacturerNormalized)
            {
                case "NOKIA":
                    return ResolveNokia(manufacturer, model);
                case "HTC":
                    return ResolveHtc(manufacturer, model);
                case "SAMSUNG":
                    return ResolveSamsung(manufacturer, model);
                case "LG":
                    return ResolveLg(manufacturer, model);
                default:
                    return new CanonicalPhoneName
                    {
                        ReportedManufacturer = manufacturer,
                        ReportedModel = model,
                        CanonicalManufacturer = manufacturer,
                        CanonicalModel = model,
                        IsResolved = false
                    };

            }
        }

        private static void Lookup(Dictionary<string, CanonicalPhoneName> lookupTable, string lookupValue, CanonicalPhoneName result)
        {
            CanonicalPhoneName modelMetadata;
            if (lookupTable.TryGetValue(lookupValue, out modelMetadata))
            {
                result.CanonicalModel = modelMetadata.CanonicalModel;
                result.Comments = modelMetadata.Comments;
                result.ScreenSize = modelMetadata.ScreenSize;
                result.OSVersion = modelMetadata.OSVersion;
                result.ScreenResolution = modelMetadata.ScreenResolution;
                result.IsResolved = true;
            }
        }

        private static CanonicalPhoneName ResolveLg(string manufacturer, string model)
        {
            var modelNormalized = model.Trim().ToUpper();

            var result = new CanonicalPhoneName
            {
                ReportedManufacturer = manufacturer,
                ReportedModel = model,
                CanonicalManufacturer = "LG",
                CanonicalModel = model,
                IsResolved = false
            };


            var lookupValue = modelNormalized;

            if (lookupValue.StartsWith("LG-C900"))
            {
                lookupValue = "LG-C900";
            }

            if (lookupValue.StartsWith("LG-E900"))
            {
                lookupValue = "LG-E900";
            }

            Lookup(lgLookupTable, lookupValue, result);

            return result;
        }

        private static CanonicalPhoneName ResolveSamsung(string manufacturer, string model)
        {
            var modelNormalized = model.Trim().ToUpper();

            var result = new CanonicalPhoneName
            {
                ReportedManufacturer = manufacturer,
                ReportedModel = model,
                CanonicalManufacturer = "SAMSUNG",
                CanonicalModel = model,
                IsResolved = false
            };


            var lookupValue = modelNormalized;

            if (lookupValue.StartsWith("GT-S7530"))
            {
                lookupValue = "GT-S7530";
            }

            if (lookupValue.StartsWith("SGH-I917"))
            {
                lookupValue = "SGH-I917";
            }

            Lookup(samsungLookupTable, lookupValue, result);

            return result;
        }

        private static CanonicalPhoneName ResolveHtc(string manufacturer, string model)
        {
            var modelNormalized = model.Trim().ToUpper();

            var result = new CanonicalPhoneName
            {
                ReportedManufacturer = manufacturer,
                ReportedModel = model,
                CanonicalManufacturer = "HTC",
                CanonicalModel = model,
                IsResolved = false
            };


            var lookupValue = modelNormalized;

            if (lookupValue.StartsWith("A620"))
            {
                lookupValue = "A620";
            }

            if (lookupValue.StartsWith("C625"))
            {
                lookupValue = "C625";
            }

            if (lookupValue.StartsWith("C620"))
            {
                lookupValue = "C625";
            }

            Lookup(htcLookupTable, lookupValue, result);

            return result;
        }

        private static CanonicalPhoneName ResolveNokia(string manufacturer, string model)
        {
            var modelNormalized = model.Trim().ToUpper();

            var result = new CanonicalPhoneName
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

            Lookup(nokiaLookupTable, lookupValue, result);

            return result;
        }

        private static Dictionary<string, CanonicalPhoneName> lgLookupTable = new Dictionary<string, CanonicalPhoneName>()
        {
            // Optimus 7Q/Quantum
            { "LG-C900", new CanonicalPhoneName(7) { CanonicalModel = "Optimus 7Q/Quantum", ScreenSize = 3.5 } },

            // Optimus 7
            { "LG-E900", new CanonicalPhoneName(7) { CanonicalModel = "Optimus 7", ScreenSize = 3.8} },

            // Jil Sander
            { "LG-E906", new CanonicalPhoneName(7) { CanonicalModel = "Jil Sander", ScreenSize = 3.8} },
        };

        private static Dictionary<string, CanonicalPhoneName> samsungLookupTable = new Dictionary<string, CanonicalPhoneName>()
        {
            // OMNIA W
            { "GT-I8350", new CanonicalPhoneName(7) { CanonicalModel = "Omnia W", ScreenSize = 3.7} },
            { "GT-I8350T", new CanonicalPhoneName(7) { CanonicalModel = "Omnia W", ScreenSize = 3.7 } },
            { "OMNIA W", new CanonicalPhoneName(7) { CanonicalModel = "Omnia W", ScreenSize = 3.7 } },

            // OMNIA 7
            { "GT-I8700", new CanonicalPhoneName(7) { CanonicalModel = "Omnia 7", ScreenSize = 4 } },
            { "OMNIA7", new CanonicalPhoneName(7) { CanonicalModel = "Omnia 7", ScreenSize = 4 } },

            // OMNIA M
            { "GT-S7530", new CanonicalPhoneName(7) { CanonicalModel = "Omnia 7", ScreenSize = 4 } },

            // Focus
            { "I917", new CanonicalPhoneName(7) { CanonicalModel = "Focus", ScreenSize = 4 } },
            { "SGH-I917", new CanonicalPhoneName(7) { CanonicalModel = "Focus", ScreenSize = 4 } },

            // Focus 2
            { "SGH-I667", new CanonicalPhoneName(7) { CanonicalModel = "Focus 2", ScreenSize = 4 } },

            // Focus Flash
            { "SGH-I677", new CanonicalPhoneName(7) { CanonicalModel = "Focus Flash", ScreenSize = 3.7 } },

            // Focus S
            { "HADEN", new CanonicalPhoneName(7) { CanonicalModel = "Focus S", ScreenSize = 4.3 } },
            { "SGH-I937", new CanonicalPhoneName(7) { CanonicalModel = "Focus S", ScreenSize = 4.3 } },

            // ATIV S
            { "GT-I8750", new CanonicalPhoneName(8) { CanonicalModel = "ATIV S", ScreenSize = 4.8, ScreenResolution = ScreenResolution._720p} },
            { "SGH-T899M", new CanonicalPhoneName(8) { CanonicalModel = "ATIV S", ScreenSize = 4.8, ScreenResolution = ScreenResolution._720p } },

            // ATIV Odyssey
            { "SCH-I930", new CanonicalPhoneName(8) { CanonicalModel = "ATIV Odyssey", ScreenSize = 4 } },
            { "SCH-R860U", new CanonicalPhoneName(8) { CanonicalModel = "ATIV Odyssey", ScreenSize = 4, Comments = "US Cellular" } },
        };

        private static Dictionary<string, CanonicalPhoneName> htcLookupTable = new Dictionary<string, CanonicalPhoneName>()
        {
            // Surround
            { "7 MONDRIAN T8788", new CanonicalPhoneName(7) { CanonicalModel = "Surround", ScreenSize = 3.8} },
            { "T8788", new CanonicalPhoneName(7) { CanonicalModel = "Surround", ScreenSize = 3.8 } },
            { "SURROUND", new CanonicalPhoneName(7) { CanonicalModel = "Surround", ScreenSize = 3.8 } },
            { "SURROUND T8788", new CanonicalPhoneName(7) { CanonicalModel = "Surround", ScreenSize = 3.8 } },

            // Mozart
            { "7 MOZART", new CanonicalPhoneName(7) { CanonicalModel = "Mozart", ScreenSize = 3.7 } },
            { "7 MOZART T8698", new CanonicalPhoneName(7) { CanonicalModel = "Mozart", ScreenSize = 3.7 } },
            { "HTC MOZART", new CanonicalPhoneName(7) { CanonicalModel = "Mozart", ScreenSize = 3.7 } },
            { "MERSAD 7 MOZART T8698", new CanonicalPhoneName(7) { CanonicalModel = "Mozart", ScreenSize = 3.7 } },
            { "MOZART", new CanonicalPhoneName(7) { CanonicalModel = "Mozart", ScreenSize = 3.7 } },
            { "MOZART T8698", new CanonicalPhoneName(7) { CanonicalModel = "Mozart", ScreenSize = 3.7 } },
            { "PD67100", new CanonicalPhoneName(7) { CanonicalModel = "Mozart", ScreenSize = 3.7 } },
            { "T8697", new CanonicalPhoneName(7) { CanonicalModel = "Mozart", ScreenSize = 3.7 } },

            // Pro
            { "7 PRO T7576", new CanonicalPhoneName(7) { CanonicalModel = "7 Pro", ScreenSize = 3.6 } },
            { "MWP6885", new CanonicalPhoneName(7) { CanonicalModel = "7 Pro", ScreenSize = 3.6 } },
            { "USCCHTC-PC93100", new CanonicalPhoneName(7) { CanonicalModel = "7 Pro", ScreenSize = 3.6 } },

            // Arrive
            { "PC93100", new CanonicalPhoneName(7) { CanonicalModel = "Arrive", ScreenSize = 3.6, Comments = "Sprint" } },
            { "T7575", new CanonicalPhoneName(7) { CanonicalModel = "Arrive", ScreenSize = 3.6, Comments = "Sprint" } },

            // HD2
            { "HD2", new CanonicalPhoneName(6) { CanonicalModel = "HD2", ScreenSize = 4.3 } },
            { "HD2 LEO", new CanonicalPhoneName(6) { CanonicalModel = "HD2", ScreenSize = 4.3 } },
            { "LEO", new CanonicalPhoneName(6) { CanonicalModel = "HD2", ScreenSize = 4.3 } },

            // HD7
            { "7 SCHUBERT T9292", new CanonicalPhoneName(7) { CanonicalModel = "HD7", ScreenSize = 4.3 } },
            { "GOLD", new CanonicalPhoneName(7) { CanonicalModel = "HD7", ScreenSize = 4.3 } },
            { "HD7", new CanonicalPhoneName(7) { CanonicalModel = "HD7", ScreenSize = 4.3 } },
            { "HD7 T9292", new CanonicalPhoneName(7) { CanonicalModel = "HD7", ScreenSize = 4.3 } },
            { "MONDRIAN", new CanonicalPhoneName(7) { CanonicalModel = "HD7", ScreenSize = 4.3 } },
            { "SCHUBERT", new CanonicalPhoneName(7) { CanonicalModel = "HD7", ScreenSize = 4.3 } },
            { "Schubert T9292", new CanonicalPhoneName(7) { CanonicalModel = "HD7", ScreenSize = 4.3 } },
            { "T9296", new CanonicalPhoneName(7) { CanonicalModel = "HD7", ScreenSize = 4.3, Comments = "Telstra, AU" } },
            { "TOUCH-IT HD7", new CanonicalPhoneName(7) { CanonicalModel = "HD7", ScreenSize = 4.3 } },

            // HD7S
            { "T9295", new CanonicalPhoneName(7) { CanonicalModel = "HD7S", ScreenSize = 4.3 } },

            // Trophy
            { "7 TROPHY", new CanonicalPhoneName(7) { CanonicalModel = "Trophy", ScreenSize = 3.8 } },
            { "7 TROPHY T8686", new CanonicalPhoneName(7) { CanonicalModel = "Trophy", ScreenSize = 3.8 } },
            { "PC40100", new CanonicalPhoneName(7) { CanonicalModel = "Trophy", ScreenSize = 3.8, Comments = "Verizon" } },
            { "SPARK", new CanonicalPhoneName(7) { CanonicalModel = "Trophy", ScreenSize = 3.8 } },
            { "TOUCH-IT TROPHY", new CanonicalPhoneName(7) { CanonicalModel = "Trophy", ScreenSize = 3.8 } },
            { "MWP6985", new CanonicalPhoneName(7) { CanonicalModel = "Trophy", ScreenSize = 3.8 } },

            // 8S
            { "A620", new CanonicalPhoneName(8) { CanonicalModel = "8S", ScreenSize = 4 } },
            { "WINDOWS PHONE 8S BY HTC", new CanonicalPhoneName(8) { CanonicalModel = "8S", ScreenSize = 4 } },

            // 8X
            { "C620", new CanonicalPhoneName(8) { CanonicalModel = "8X", ScreenSize = 4.3, ScreenResolution = ScreenResolution._720p} },
            { "C625", new CanonicalPhoneName(8) { CanonicalModel = "8X", ScreenSize = 4.3, ScreenResolution = ScreenResolution._720p } },
            { "HTC6990LVW", new CanonicalPhoneName(8) { CanonicalModel = "8X", ScreenSize = 4.3, ScreenResolution = ScreenResolution._720p, Comments="Verizon" } },
            { "PM23300", new CanonicalPhoneName(8) { CanonicalModel = "8X", ScreenSize = 4.3, ScreenResolution = ScreenResolution._720p, Comments="AT&T" } },
            { "WINDOWS PHONE 8X BY HTC", new CanonicalPhoneName(8) { CanonicalModel = "8X", ScreenSize = 4.3, ScreenResolution = ScreenResolution._720p } },

            // 8XT
            { "HTCPO881 SPRINT", new CanonicalPhoneName(8) { CanonicalModel = "8XT", ScreenSize = 4.3, Comments="Sprint" } },

            // Titan
            { "ETERNITY", new CanonicalPhoneName(7) { CanonicalModel = "Titan", ScreenSize = 4.7, Comments = "China" } },
            { "PI39100", new CanonicalPhoneName(7) { CanonicalModel = "Titan", ScreenSize = 4.7, Comments = "AT&T" } },
            { "TITAN X310E", new CanonicalPhoneName(7) { CanonicalModel = "Titan", ScreenSize = 4.7 } },
            { "ULTIMATE", new CanonicalPhoneName(7) { CanonicalModel = "Titan", ScreenSize = 4.7 } },
            { "X310E", new CanonicalPhoneName(7) { CanonicalModel = "Titan", ScreenSize = 4.7 } },
            { "X310E TITAN", new CanonicalPhoneName(7) { CanonicalModel = "Titan", ScreenSize = 4.7 } },
            
            // Titan II
            { "PI86100", new CanonicalPhoneName(7) { CanonicalModel = "Titan II", ScreenSize = 4.7, Comments = "AT&T" } },
            { "RADIANT", new CanonicalPhoneName(7) { CanonicalModel = "Titan II", ScreenSize = 4.7 } },

            // Radar
            { "RADAR", new CanonicalPhoneName(7) { CanonicalModel = "Radar", ScreenSize = 3.8 } },
            { "RADAR 4G", new CanonicalPhoneName(7) { CanonicalModel = "Radar", ScreenSize = 3.8, Comments = "T-Mobile USA" } },
            { "RADAR C110E", new CanonicalPhoneName(7) { CanonicalModel = "Radar", ScreenSize = 3.8 } },
            
        };

        private static Dictionary<string, CanonicalPhoneName> nokiaLookupTable = new Dictionary<string, CanonicalPhoneName>()
        {
            // Lumia 505
            { "LUMIA 505", new CanonicalPhoneName(7) { CanonicalModel = "Lumia 505", ScreenSize = 3.7 } },
            // Lumia 510
            { "LUMIA 510", new CanonicalPhoneName(7) { CanonicalModel = "Lumia 510", ScreenSize = 4 } },
            { "NOKIA 510", new CanonicalPhoneName(7) { CanonicalModel = "Lumia 510", ScreenSize = 4 } },
            // Lumia 610
            { "LUMIA 610", new CanonicalPhoneName(7) { CanonicalModel = "Lumia 610", ScreenSize = 3.7 } },
            { "LUMIA 610 NFC", new CanonicalPhoneName(7) { CanonicalModel = "Lumia 610", ScreenSize = 3.7, Comments = "NFC" } },
            { "NOKIA 610", new CanonicalPhoneName(7) { CanonicalModel = "Lumia 610", ScreenSize = 3.7 } },
            { "NOKIA 610C", new CanonicalPhoneName(7) { CanonicalModel = "Lumia 610", ScreenSize = 3.7 } },
            // Lumia 620
            { "LUMIA 620", new CanonicalPhoneName(8) { CanonicalModel = "Lumia 620", ScreenSize = 3.8 } },
            { "RM-846", new CanonicalPhoneName(8) { CanonicalModel = "Lumia 620", ScreenSize = 3.8 } },
            // Lumia 710
            { "LUMIA 710", new CanonicalPhoneName(7) { CanonicalModel = "Lumia 710", ScreenSize = 3.7 } },
            { "NOKIA 710", new CanonicalPhoneName(7) { CanonicalModel = "Lumia 710", ScreenSize = 3.7 } },
            // Lumia 800
            { "LUMIA 800", new CanonicalPhoneName(7) { CanonicalModel = "Lumia 800", ScreenSize = 3.7 } },
            { "LUMIA 800C", new CanonicalPhoneName(7) { CanonicalModel = "Lumia 800", ScreenSize = 3.7 } },
            { "NOKIA 800", new CanonicalPhoneName(7) { CanonicalModel = "Lumia 800", ScreenSize = 3.7 } },
            { "NOKIA 800C", new CanonicalPhoneName(7) { CanonicalModel = "Lumia 800", ScreenSize = 3.7, Comments = "China" } },
            // Lumia 810
            { "RM-878", new CanonicalPhoneName(8) { CanonicalModel = "Lumia 810", ScreenSize = 4.3 } },
            // Lumia 820
            { "RM-824", new CanonicalPhoneName(8) { CanonicalModel = "Lumia 820", ScreenSize = 4.3 } },
            { "RM-825", new CanonicalPhoneName(8) { CanonicalModel = "Lumia 820", ScreenSize = 4.3 } },
            { "RM-826", new CanonicalPhoneName(8) { CanonicalModel = "Lumia 820", ScreenSize = 4.3 } },
            // Lumia 822
            { "RM-845", new CanonicalPhoneName(8) { CanonicalModel = "Lumia 822", ScreenSize = 3.7, Comments = "Verizon" } },
            // Lumia 900
            { "LUMIA 900", new CanonicalPhoneName(7) { CanonicalModel = "Lumia 900", ScreenSize = 3.7 } },
            { "NOKIA 900", new CanonicalPhoneName(7) { CanonicalModel = "Lumia 900", ScreenSize = 3.7 } },
            // Lumia 920
            { "RM-820", new CanonicalPhoneName(8) { CanonicalModel = "Lumia 920", ScreenSize = 4.5, ScreenResolution = ScreenResolution.WXGA } },
            { "RM-821", new CanonicalPhoneName(8) { CanonicalModel = "Lumia 920", ScreenSize = 4.5, ScreenResolution = ScreenResolution.WXGA } },
            { "RM-822", new CanonicalPhoneName(8) { CanonicalModel = "Lumia 920", ScreenSize = 4.5, ScreenResolution = ScreenResolution.WXGA } },
            { "RM-867", new CanonicalPhoneName(8) { CanonicalModel = "Lumia 920", ScreenSize = 4.5, ScreenResolution = ScreenResolution.WXGA, Comments = "920T" } },
            { "NOKIA 920", new CanonicalPhoneName(8) { CanonicalModel = "Lumia 920", ScreenSize = 4.5, ScreenResolution = ScreenResolution.WXGA } },
            { "LUMIA 920", new CanonicalPhoneName(8) { CanonicalModel = "Lumia 920", ScreenSize = 4.5, ScreenResolution = ScreenResolution.WXGA } },
            // Lumia 520
            { "RM-914", new CanonicalPhoneName(8) { CanonicalModel = "Lumia 520", ScreenSize = 4 } },
            { "RM-915", new CanonicalPhoneName(8) { CanonicalModel = "Lumia 520", ScreenSize = 4 } },
            { "RM-913", new CanonicalPhoneName(8) { CanonicalModel = "Lumia 520", ScreenSize = 4, Comments="520T" } },
            // Lumia 521?
            { "RM-917", new CanonicalPhoneName(8) { CanonicalModel = "Lumia 521", ScreenSize = 4, Comments="T-Mobile 520" } },
            // Lumia 720
            { "RM-885", new CanonicalPhoneName(8) { CanonicalModel = "Lumia 720", ScreenSize = 4.3 } },
            { "RM-887", new CanonicalPhoneName(8) { CanonicalModel = "Lumia 720", ScreenSize = 4.3, Comments="China 720T" } },
            // Lumia 928
            { "RM-860", new CanonicalPhoneName(8) { CanonicalModel = "Lumia 928", ScreenSize = 4.5, ScreenResolution = ScreenResolution.WXGA } },
            // Lumia 925
            { "RM-892", new CanonicalPhoneName(8) { CanonicalModel = "Lumia 925", ScreenSize = 4.5, ScreenResolution = ScreenResolution.WXGA } },
            { "RM-893", new CanonicalPhoneName(8) { CanonicalModel = "Lumia 925", ScreenSize = 4.5, ScreenResolution = ScreenResolution.WXGA } },
            { "RM-910", new CanonicalPhoneName(8) { CanonicalModel = "Lumia 925", ScreenSize = 4.5, ScreenResolution = ScreenResolution.WXGA, Comments="China 925T" } },
            // Lumia 1020
            { "RM-875", new CanonicalPhoneName(8) { CanonicalModel = "Lumia 1020", ScreenSize = 4.5, ScreenResolution = ScreenResolution.WXGA } },
            { "RM-876", new CanonicalPhoneName(8) { CanonicalModel = "Lumia 1020", ScreenSize = 4.5, ScreenResolution = ScreenResolution.WXGA } },
            { "RM-877", new CanonicalPhoneName(8) { CanonicalModel = "Lumia 1020", ScreenSize = 4.5, ScreenResolution = ScreenResolution.WXGA } },
            // Lumia 625
            { "RM-941", new CanonicalPhoneName(8) { CanonicalModel = "Lumia 625", ScreenSize = 4.7 } },
            { "RM-942", new CanonicalPhoneName(8) { CanonicalModel = "Lumia 625", ScreenSize = 4.7 } },
            { "RM-943", new CanonicalPhoneName(8) { CanonicalModel = "Lumia 625", ScreenSize = 4.7 } },
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
        public double ScreenSize { get; set; }
        public int OSVersion { get; set; }
        public ScreenResolution ScreenResolution { get; set; }

        public string FullCanonicalName
        {
            get { return CanonicalManufacturer + " " + CanonicalModel; }
        }

        public CanonicalPhoneName()
        {
        }

        public CanonicalPhoneName(int osVersion)
        {
            OSVersion = osVersion;

            // all windows phone 7 devices have the same resolution
            // we will also default WP8 devices to the same resolution
            ScreenResolution = ScreenResolution.WVGA;
        }
    }
}
