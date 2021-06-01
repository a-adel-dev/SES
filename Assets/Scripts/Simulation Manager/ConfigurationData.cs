using UnityEngine;
using System.IO;
using System;

namespace SES.SimManager
{
    public class ConfigurationData
    {
        #region properties
        const string ConfigurationDataFileName = "ConfigurationData.csv";

        //config data goes here
        public float TimeStep { get; set; } = 4f;
        public int SimulationTotalLength { get; set; } = 14;
        public int NumberOfSchoolDayPeriods { get; set; } = 4;
        public int PeriodLength { get; set; } = 40;
        public int BreakLength { get; set; } = 10;
        public bool ActivitiesEnabled { get; set; } = true;
        public bool RelocationEnabled { get; set; } = true;
        public int InitialNumStudentsContagious { get; set; } = 0;
        public int InitialNumTeachersContagious { get; set; } = 0;
        public int CooldownClassExit { get; set; } = 0;
        public int StudentsMaskSettings { get; set; } = 0;
        public int TeacherMaskSettings { get; set; } = 0;
        public bool SchoolHalfCapacity { get; set; } = false;
        public bool ClassroomHalfCapacity { get; set; } = false;
        public int AirControlSettings { get; set; } = 0;

        public int MinClassSectionNumber { get; set; } = 1;
        public int MaxClassSectionNumber { get; set; } = 8;
        public int MinClassActivityTime { get; set; } = 8;

        public float AdultWalkingSpeed { get; set; } = 1.5f;
        public float ChildrenWalkingSpeed { get; set; } = 0.6f;
        public int BaseAutonomyChance { get; set; } = 10;
        public int BreakAutonomyChance { get; set; } = 20;

        public int NumSpotsForGroupActivity { get; set; } = 4;
        public float DeskGroupActivityCompensationX { get; set; } = 0f;
        public float DeskGroupActivityCompensationZ { get; set; } = -0.5f;
        public float MinDistanceGroupActivity { get; set; } = 3f;

        public int BathroomChance { get; set; } = 2;
        public float LockerWaitingTime { get; set; } = 2f;

        //general
        /// <summary>
        /// The critical radius after which larger radius are not counted
        /// in the sim Cᵣ(μm)
        /// </summary>
        public float CriticalRadius { get; set; } = 2.6f;
        /// <summary>
        /// the concentration of suspended pathogen to the infection 
        /// rate Cᵢ(infection quanta)
        /// </summary>
        public float ViralInfectivity { get; set; } = 0.1f;

        //Breathing flow
        public float NormalBreathingFlowRate { get; set; } = 0.5f;//Breathing flow rate assumed for mouth breathing Q_b (m3/h)
        public float TalkingBreathingFlowRate { get; set; } = 0.75f;//Breathing flow rate assumed for talking Q_b (m3/h)
        public float LoudtalkingBreathingFlowRate { get; set; } = 1.0f;//Breathing flow rate assumed for shouting (m3/h)

        //Breathing droplet concentration
        public float AvarageNaturalDropletConentration { get; set; } = 0.1f;//Breathing droplet concentration (cm-3)
        public float AvarageTalkingDropletConcentration { get; set; } = 0.3f;// Talking droplet concentration(cm-3)
        public float AvarageShoutingDropletConcentration { get; set; } = 0.9f; //Shouting droplet concentration(cm-3)"

        //Virion parameters
        /// <summary>
        /// Concentration of virions in the droplets (copies/liquid volume)
        /// </summary>
        public float ViralLoad { get; set; } = 10E11f;

        //Short range infection parameters
        /// <summary>
        /// The jet entrainment coefficient which is typically fall in
        /// the range for the turbulent jet 0.1 - 0.15 (α)
        /// </summary>
        public float JetEntrainmentCoefficient { get; set; } = 0.1f;
        public float MouthArea { get; set; } = 2f;//"Mouth area (cm^2")

        //Space parameters
        public int InitialAirExchangeRate { get; set; } = 3;

        //Mask Parameters
        public float N95MaskValue { get; set; } = 0.05f;
        public float SurgicalMaskValue { get; set; } = 0.15f;
        public float ClothMaskValue { get; set; } = 0.8f;

        //Visualization Parameters
        /// <summary>
        /// how fast a space is considerred to be totally contaminated
        /// </summary>
        public float SpaceInfectionThreshold { get; set; } = .001f;

        //infection Parameters
        /// <summary>
        /// Time before infected turns to contagious, in hours, minutes, seconds
        /// </summary>
        //originally (66,0,0)
        public TimeSpan TimeBeforeContagious { get; set; } = new TimeSpan(0, 10, 0);
        int hoursTimeBeforeContagious = 0;
        int minutesTimeBeforeContagios = 10;
        #endregion

        public ConfigurationData()
        {
            StreamReader input = null;
            try
            {
                input = File.OpenText(Path.Combine(
                    Application.streamingAssetsPath, ConfigurationDataFileName));

                string names = input.ReadLine();
                string values = input.ReadLine();

                SetConfigurationDataFields(values);
            }
            catch (Exception e)
            {

            }
            finally
            {
                if (input != null)
                {
                    input.Close();
                }
            }
        }

        private void SetConfigurationDataFields(string csvValues)
        {
            string[] values = csvValues.Split(',');

            TimeStep = float.Parse(values[0]);
            SimulationTotalLength = int.Parse(values[1]);
            NumberOfSchoolDayPeriods = int.Parse(values[2]);
            PeriodLength = int.Parse(values[3]);
            BreakLength = int.Parse(values[4]);
            ActivitiesEnabled = int.Parse(values[5]) == 1;
            RelocationEnabled = int.Parse(values[6]) == 1;
            InitialNumStudentsContagious = int.Parse(values[7]);
            InitialNumTeachersContagious = int.Parse(values[8]);
            CooldownClassExit = int.Parse(values[9]);
            StudentsMaskSettings = int.Parse(values[10]);
            TeacherMaskSettings = int.Parse(values[11]);
            SchoolHalfCapacity = int.Parse(values[12]) == 1;
            ClassroomHalfCapacity = int.Parse(values[13]) == 1;
            AirControlSettings = int.Parse(values[14]);

            MinClassSectionNumber = int.Parse(values[15]);
            MaxClassSectionNumber = int.Parse(values[16]);
            MinClassActivityTime = int.Parse(values[17]);
            AdultWalkingSpeed = float.Parse(values[18]);
            ChildrenWalkingSpeed = float.Parse(values[19]);
            BaseAutonomyChance = int.Parse(values[20]);
            BreakAutonomyChance = int.Parse(values[21]);
            NumSpotsForGroupActivity = int.Parse(values[22]);
            DeskGroupActivityCompensationX = float.Parse(values[23]);
            DeskGroupActivityCompensationZ = float.Parse(values[24]);
            MinDistanceGroupActivity = float.Parse(values[25]);
            BathroomChance = int.Parse(values[26]);
            LockerWaitingTime = float.Parse(values[27]);

            CriticalRadius = float.Parse(values[28]);
            ViralInfectivity = float.Parse(values[29]);
            NormalBreathingFlowRate = float.Parse(values[30]);
            TalkingBreathingFlowRate = float.Parse(values[31]);
            LoudtalkingBreathingFlowRate = float.Parse(values[32]);
            AvarageNaturalDropletConentration = float.Parse(values[33]);
            AvarageTalkingDropletConcentration = float.Parse(values[34]);
            AvarageShoutingDropletConcentration = float.Parse(values[35]);
            ViralLoad = float.Parse(values[36]);
            JetEntrainmentCoefficient = float.Parse(values[37]);
            MouthArea = float.Parse(values[38]);
            InitialAirExchangeRate = int.Parse(values[39]);
            N95MaskValue = float.Parse(values[40]);
            SurgicalMaskValue = float.Parse(values[41]);
            ClothMaskValue = float.Parse(values[42]);
            SpaceInfectionThreshold = float.Parse(values[43]);
            hoursTimeBeforeContagious = int.Parse(values[44]);
            minutesTimeBeforeContagios = int.Parse(values[45]);
            TimeBeforeContagious = new TimeSpan(hoursTimeBeforeContagious, minutesTimeBeforeContagios, 0);
        }
    }
}
