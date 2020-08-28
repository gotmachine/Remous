using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remous
{
    public class FrequencySource
    {
        public readonly double minFreq;
        public readonly double maxFreq;
        public readonly string name;
        public readonly List<SourceOperator> operators;

        public FrequencySource(double minFreq, double maxFreq, string name, List<SourceOperator> operators = null)
        {
            this.minFreq = minFreq;
            this.maxFreq = maxFreq;
            this.name = name;
            this.operators = operators;
        }
    }

    public class SourceOperator
    {
        public readonly double minFreq;
        public readonly double maxFreq;
        public readonly Operator sourceOperator;

        public SourceOperator(double minFreq, double maxFreq, Operator sourceOperator)
        {
            this.minFreq = minFreq;
            this.maxFreq = maxFreq;
            this.sourceOperator = sourceOperator;
        }
    }

    public class Operator
    {
        public readonly string name;

        public Operator(string name)
        {
            this.name = name;
        }
    }

    public static class FrequencyAnalyzer
    {
        public static Operator SFR = new Operator("SFR");
        public static Operator Orange = new Operator("Orange");
        public static Operator Bouygues = new Operator("Bouygues");
        public static Operator Free = new Operator("Free");
        public static Operator SNCF = new Operator("SNCF");


        public static List<FrequencySource> frequencySources = new List<FrequencySource>()
        {
            new FrequencySource(225, 326.5, "Aéronautique militaire, aérospatial"),
            new FrequencySource(326.5, 328.5, "Radioastronomie"),
            new FrequencySource(238.5, 328.6, "Aéronautique militaire"),
            new FrequencySource(328.6, 335.4, "Radionavigation aéronautique"),
            new FrequencySource(335.4, 400, "Aéronautique civile et militaire"),
            new FrequencySource(401, 402, "Balises satellite ARGOS"),
            new FrequencySource(406, 407, "Radiobalises"),
            new FrequencySource(430, 447, "Radio amateur"),
            new FrequencySource(470, 694, "Télévision TNT"),
            new FrequencySource(703, 862, "Mobile 4G", new List<SourceOperator>()
            {
                new SourceOperator(703, 708, SFR),
                new SourceOperator(708, 718, Orange),
                new SourceOperator(718, 723, Bouygues),
                new SourceOperator(723, 733, Free),
                new SourceOperator(758, 763, SFR),
                new SourceOperator(763, 773, Orange),
                new SourceOperator(773, 778, Bouygues),
                new SourceOperator(778, 788, Free),
                new SourceOperator(791, 801, Bouygues),
                new SourceOperator(801, 811, SFR),
                new SourceOperator(811, 821, Orange),
                new SourceOperator(832, 842, Bouygues),
                new SourceOperator(842, 852, SFR),
                new SourceOperator(852, 862, Orange)
            }),
            new FrequencySource(863, 865, "Microphones sans fil"),
            new FrequencySource(865, 868, "Puces RFID"),
            new FrequencySource(876, 960, "Mobile 3G", new List<SourceOperator>()
            {
                new SourceOperator(876, 880, SNCF),
                new SourceOperator(880, 890, Bouygues),
                new SourceOperator(890, 900, Orange),
                new SourceOperator(900, 905, Free),
                new SourceOperator(905, 914, SFR),
                new SourceOperator(921, 925, SNCF),
                new SourceOperator(925, 935, Bouygues),
                new SourceOperator(935, 945, Orange),
                new SourceOperator(945, 950, Free),
                new SourceOperator(950, 960, SFR)
            }),
            new FrequencySource(960, 1215, "Navigation Aéronautique"),
            new FrequencySource(1215, 1240, "GPS militaire"),
            new FrequencySource(1240, 1256, "GLONASS "),
            new FrequencySource(1260, 1300, "Radioamateur"),
            new FrequencySource(1525, 1559, "Mobile satellitaire"),
            new FrequencySource(1563, 1587, "GPS civil, Galileo"),
            new FrequencySource(1591, 1610, "GLONASS (GPS Russe)"),
            new FrequencySource(1610, 1660, "Mobile satellitaire"),
            new FrequencySource(1660, 1710, "Métérologie, astronomie"),
            new FrequencySource(1710, 1880, "Mobile 2G/4G", new List<SourceOperator>()
            {
                new SourceOperator(1710, 1730, Orange),
                new SourceOperator(1730, 1750, SFR),
                new SourceOperator(1750, 1765, Free),
                new SourceOperator(1765, 1785, Bouygues),
                new SourceOperator(1805, 1825, Orange),
                new SourceOperator(1825, 1845, SFR),
                new SourceOperator(1845, 1860, Free),
                new SourceOperator(1860, 1880, Bouygues)
            }),
            new FrequencySource(1880, 1920, "DECT (téléphonie fixe sans fil)"),
            new FrequencySource(1920, 1980, "Mobile 3G/4G", new List<SourceOperator>()
            {
                new SourceOperator(1920, 1935, SFR),
                new SourceOperator(1935, 1950, Bouygues),
                new SourceOperator(1950, 1965, Free),
                new SourceOperator(1965, 1980, Orange)
            }),
            new FrequencySource(1980, 2110, "Mobile satellitaire"),
            new FrequencySource(2110, 2170, "Mobile 3G/4G", new List<SourceOperator>()
            {
                new SourceOperator(2110, 2125, SFR),
                new SourceOperator(2125, 2140, Bouygues),
                new SourceOperator(2140, 2155, Free),
                new SourceOperator(2155, 2170, Orange)
            }),
            new FrequencySource(2170, 2200, "Mobile satellitaire"),
            new FrequencySource(2400, 2484, "Wifi / Bluetooth / Domotique"),
            new FrequencySource(2500, 2690, "Mobile 4G", new List<SourceOperator>()
            {
                new SourceOperator(2500, 2515, SFR),
                new SourceOperator(2515, 2535, Orange),
                new SourceOperator(2535, 2550, Bouygues),
                new SourceOperator(2550, 2570, Free),
                new SourceOperator(2620, 2635, SFR),
                new SourceOperator(2635, 2655, Orange),
                new SourceOperator(2655, 2670, Bouygues),
                new SourceOperator(2670, 2690, Free),

            }),
            new FrequencySource(2700, 3400, "Radars S-band"),
            new FrequencySource(3400, 3600, "DECT (téléphonie fixe sans fil)"),
            new FrequencySource(3600, 3800, "Réseau TV MMDS"),
            new FrequencySource(3800, 4200, "TV par satellite"),
            new FrequencySource(4200, 5090, "Réseaux locaux spécialisés"),
            new FrequencySource(5150, 5350, "Wifi"),
            new FrequencySource(5400, 5725, "Wifi")
        };

        public static bool AnalyzeFrequency(double frequency, out FrequencySource source, out Operator sourceOperator)
        {
            foreach (FrequencySource checkedSource in frequencySources)
            {
                if (frequency > checkedSource.minFreq && frequency <= checkedSource.maxFreq)
                {
                    source = checkedSource;
                    sourceOperator = null;

                    if (checkedSource.operators != null)
                    {
                        foreach (SourceOperator checkedOperator in checkedSource.operators)
                        {
                            if (frequency > checkedOperator.minFreq && frequency <= checkedOperator.maxFreq)
                            {
                                sourceOperator = checkedOperator.sourceOperator;
                                break;
                            }
                        }
                    }

                    return true;
                }
            }

            source = null;
            sourceOperator = null;
            return false;
        }
    }
}
