//
// This autonomous intelligent system software is the property of Cartheur Research B.V. Copyright 2021 - 2025, all rights reserved.
//
#define windows
using Cartheur.Animals.Robot;
using ConsoleTables;
#if windows
using System.Speech.Recognition;
using System.Speech.Synthesis;
#endif

class PoseReader
{
    public static SettingsDictionary GlobalSettings { get; set; }
    public static string OutputFileName { get; set; }
    public static bool UseVoiceControl { get; set; }
    public static string UserInput { get; set; }
    public static MotorFunctions MotorControl { get; set; }
    public static MotorSequence MotorSequenceAll { get; set; }
    public static MotorSequence MotorSequenceAbdomen { get; set; }
    public static MotorSequence MotorSequenceBust { get; set; }
    public static MotorSequence MotorSequenceLeftArm { get; set; }
    public static MotorSequence MotorSequenceLeftLeg { get; set; }
    public static MotorSequence MotorSequenceRightArm { get; set; }
    public static MotorSequence MotorSequenceRightLeg { get; set; }
    public static bool MotorsInitialized { get; set; }

#if windows
    static readonly SpeechRecognitionEngine Recognizer = new SpeechRecognitionEngine();
    static readonly GrammarBuilder GrammarBuilder = new GrammarBuilder();
    static readonly SpeechSynthesizer SpeechSynth = new SpeechSynthesizer();
    static readonly PromptBuilder PromptBuilder = new PromptBuilder();
#endif

    static async Task LoadSettings()
    {
        var path = Path.Combine(Environment.CurrentDirectory, Path.Combine("config", "Settings.xml"));
        GlobalSettings.LoadSettings(path);
        await Task.CompletedTask;
    }

    static async Task Freeze(Limbic.LimbicArea area)
    {
        switch (area)
        {
            case Limbic.LimbicArea.Abdomen:
                MotorControl.SetTorqueOn(Limbic.Abdomen);
                break;
            case Limbic.LimbicArea.All:
                MotorControl.SetTorqueOn("all");
                break;
            case Limbic.LimbicArea.Bust:
                MotorControl.SetTorqueOn(Limbic.Bust);
                break;
            case Limbic.LimbicArea.Head:
                MotorControl.SetTorqueOn(Limbic.Head);
                break;
            case Limbic.LimbicArea.LeftArm:
                MotorControl.SetTorqueOn(Limbic.LeftArm);
                break;
            case Limbic.LimbicArea.RightArm:
                MotorControl.SetTorqueOn(Limbic.RightArm);
                break;
            case Limbic.LimbicArea.LeftPelvis:
                MotorControl.SetTorqueOn(Limbic.LeftPelvis);
                break;
            case Limbic.LimbicArea.RightPelvis:
                MotorControl.SetTorqueOn(Limbic.RightPelvis);
                break;
            case Limbic.LimbicArea.LeftLeg:
                MotorControl.SetTorqueOn(Limbic.LeftLeg);
                break;
            case Limbic.LimbicArea.RightLeg:
                MotorControl.SetTorqueOn(Limbic.RightLeg);
                break;
            default:
                break;
        }
        await Task.CompletedTask;
    }

    static async Task Unfreeze(Limbic.LimbicArea area)
    {
        switch (area)
        {
            case Limbic.LimbicArea.Abdomen:
                MotorControl.SetTorqueOff(Limbic.Abdomen);
                break;
            case Limbic.LimbicArea.All:
                MotorControl.SetTorqueOff("all");
                break;
            case Limbic.LimbicArea.Bust:
                MotorControl.SetTorqueOff(Limbic.Bust);
                break;
            case Limbic.LimbicArea.Head:
                MotorControl.SetTorqueOff(Limbic.Head);
                break;
            case Limbic.LimbicArea.LeftArm:
                MotorControl.SetTorqueOff(Limbic.LeftArm);
                break;
            case Limbic.LimbicArea.RightArm:
                MotorControl.SetTorqueOff(Limbic.RightArm);
                break;
            case Limbic.LimbicArea.LeftPelvis:
                MotorControl.SetTorqueOff(Limbic.LeftPelvis);
                break;
            case Limbic.LimbicArea.RightPelvis:
                MotorControl.SetTorqueOff(Limbic.RightPelvis);
                break;
            case Limbic.LimbicArea.LeftLeg:
                MotorControl.SetTorqueOff(Limbic.LeftLeg);
                break;
            case Limbic.LimbicArea.RightLeg:
                MotorControl.SetTorqueOff(Limbic.RightLeg);
                break;
            default:
                break;
        }
        await Task.CompletedTask;
    }

    static async Task Scan(Limbic.LimbicArea area)
    {
        switch (area)
        {
            case Limbic.LimbicArea.Abdomen:
                var abdomen = MotorSequenceAbdomen.ReturnDictionaryOfPositions(Limbic.Abdomen);
                var tableAbdomen = new ConsoleTable("abdomen", "position");
                foreach (var kvpa in abdomen)
                {
                    tableAbdomen.AddRow(kvpa.Key, kvpa.Value);
                }
                tableAbdomen.Write();
                Console.WriteLine();
                Logging.WriteLog(tableAbdomen.ToString(), Logging.LogType.Data, Logging.LogCaller.JoiPose);
                break;
            case Limbic.LimbicArea.Bust:
                var bust = MotorSequenceBust.ReturnDictionaryOfPositions(Limbic.Bust);
                var tableBust = new ConsoleTable("bust", "position");
                foreach (var kvpb in bust)
                {
                    tableBust.AddRow(kvpb.Key, kvpb.Value);
                }
                tableBust.Write();
                Console.WriteLine();
                Logging.WriteLog(tableBust.ToString(), Logging.LogType.Data, Logging.LogCaller.JoiPose);
                break;
            case Limbic.LimbicArea.Head:

                break;
            case Limbic.LimbicArea.LeftArm:
                var leftArm = MotorSequenceLeftArm.ReturnDictionaryOfPositions(Limbic.LeftArm);
                var tableLeftArm = new ConsoleTable("leftarm", "position");
                foreach (var kvpla in leftArm)
                {
                    tableLeftArm.AddRow(kvpla.Key, kvpla.Value);
                }
                tableLeftArm.Write();
                Console.WriteLine();
                Logging.WriteLog(tableLeftArm.ToString(), Logging.LogType.Data, Logging.LogCaller.JoiPose);
                break;
            case Limbic.LimbicArea.RightArm:
                var rightArm = MotorSequenceRightArm.ReturnDictionaryOfPositions(Limbic.RightArm);
                var tablerightArm = new ConsoleTable("rightarm", "position");
                foreach (var kvpr in rightArm)
                {
                    tablerightArm.AddRow(kvpr.Key, kvpr.Value);
                }
                tablerightArm.Write();
                Console.WriteLine();
                Logging.WriteLog(tablerightArm.ToString(), Logging.LogType.Data, Logging.LogCaller.JoiPose);
                break;
            case Limbic.LimbicArea.LeftPelvis:
                Console.WriteLine("Implemented as a part of the entire leg.");
                break;
            case Limbic.LimbicArea.RightPelvis:
                Console.WriteLine("Implemented as a part of the entire leg.");
                break;
            case Limbic.LimbicArea.LeftLeg:
                var leftLeg = MotorSequenceLeftLeg.ReturnDictionaryOfPositions(Limbic.LeftLeg);
                var tableLeftLeg = new ConsoleTable("leftleg", "position");
                foreach (var kvpl in leftLeg)
                {
                    tableLeftLeg.AddRow(kvpl.Key, kvpl.Value);
                }
                tableLeftLeg.Write();
                Console.WriteLine();
                Logging.WriteLog(tableLeftLeg.ToString(), Logging.LogType.Data, Logging.LogCaller.JoiPose);
                break;
            case Limbic.LimbicArea.RightLeg:
                var rightLeg = MotorSequenceRightLeg.ReturnDictionaryOfPositions(Limbic.RightLeg);
                var tablerightLeg = new ConsoleTable("rightleg", "position");
                foreach (var kvpr in rightLeg)
                {
                    tablerightLeg.AddRow(kvpr.Key, kvpr.Value);
                }
                tablerightLeg.Write();
                Console.WriteLine();
                Logging.WriteLog(tablerightLeg.ToString(), Logging.LogType.Data, Logging.LogCaller.JoiPose);
                break;
            default:
                break;
        }
        await Task.CompletedTask;
    }
#if windows
    static async Task InitializeSapi()
    {
        GrammarBuilder.Culture = Recognizer.RecognizerInfo.Culture;
        try
        {
            Choices choices = new Choices();
            //GlobalSettings.GrabSetting("voicegrammar");
            choices.Add(new string[] { "do", "scan Abdomen", "scan Bust", "scan LeftArm", "scan RightArm", "scan LeftLeg", "scan RightLeg", "freeze Abdomen", "freeze Bust", "freeze LeftArm", "freeze RightArm", "freeze LeftLeg", "freeze RightLeg", "unfreeze Abdomen", "unfreeze Bust", "unfreeze LeftArm", "unfreeze RightArm", "unfreeze LeftLeg", "unfreeze RightLeg", "program quit", "give me a list of what I can do", "what are the areas of the robot" });

            GrammarBuilder.Append(choices);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        var grammar = new Grammar(GrammarBuilder);
        try
        {
            Recognizer.UnloadAllGrammars();
            Recognizer.RecognizeAsyncCancel();
            Recognizer.RequestRecognizerUpdate();
            Recognizer.LoadGrammar(grammar);
            Recognizer.SpeechRecognized += SapiWindowsSpeechRecognized;
            Recognizer.SetInputToDefaultAudioDevice();
            Recognizer.RecognizeAsync(RecognizeMode.Multiple);
            Logging.WriteLog("Windows SAPI: Recognizer initialized.", Logging.LogType.Information, Logging.LogCaller.JoiPose);
            Console.WriteLine("Windows SAPI: Recognizer initialized.");
            Console.WriteLine("For help, speak the phrase: \"give me a list of what I can do\".");
            Console.WriteLine("To get a list of supported area operations, speak the phrase: \"what are the areas of the robot\".");
            Console.WriteLine("Speak the phrase: \"program quit\" to exit the application.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error has occurred in the recognizer. " + ex.Message);
            Logging.WriteLog(ex.Message, Logging.LogType.Error, Logging.LogCaller.JoiPose);
        }
        finally
        {
            Console.WriteLine("Ready for pose operations.");
            await SpeakText("Ready to do pose engineering!");
        }
        await Task.CompletedTask;
    }

    static async Task SpeakText(string input)
    {
        try
        {
            PromptBuilder.ClearContent();
            PromptBuilder.AppendText(input);
            SpeechSynth.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Adult);
            SpeechSynth.Speak(PromptBuilder);
        }
        catch (Exception ex)
        {
            Logging.WriteLog(ex.Message, Logging.LogType.Error, Logging.LogCaller.JoiPose);
        }
        await Task.CompletedTask;
    }
#endif
    static async Task Main()
    {
        GlobalSettings = new SettingsDictionary();
        await LoadSettings();
        OutputFileName = GlobalSettings.GrabSetting("outputfile");
        UseVoiceControl = Convert.ToBoolean(GlobalSettings.GrabSetting("voicecontrol"));
        MotorControl = new MotorFunctions();
        MotorSequenceAll = new MotorSequence();
        MotorSequenceAbdomen = new MotorSequence();
        MotorSequenceBust = new MotorSequence();
        MotorSequenceLeftArm = new MotorSequence();
        MotorSequenceLeftLeg = new MotorSequence();
        MotorSequenceRightArm = new MotorSequence();
        MotorSequenceRightLeg = new MotorSequence();
        Console.WriteLine(MotorFunctions.InitializeDynamixelMotors());
        MotorsInitialized = MotorFunctions.DynamixelMotorsInitialized;
        MotorFunctions.CollateMotorArray();

        if (MotorsInitialized) { MotorControl.CreateConnectMotorObjects(); }
        else Logging.WriteLog("Cannot create connection objects.", Logging.LogType.Error, Logging.LogCaller.MotorControl);

        if (UseVoiceControl)
        {
            #if windows
            await InitializeSapi();
            await SpeakText("Hold the robot in the desired pose for joint capture, then tell me what you would like to do.");
#endif
            while (true)
            {
                UserInput = Console.ReadLine();
                if (UserInput == "program quit")
                    await SpeakText("Detected quit. Closing the application.");
                break;
            }
        }
        if (!UseVoiceControl)
        {
            // Program help.
            Console.WriteLine("Hold the robot in the desired pose for motor capture, then press Enter...");
            Console.WriteLine("For reference: Limbic areas are: Abdomen, Bust, Head, LeftArm, RightArm, LeftLeg, RightLeg.");
            Console.WriteLine("The current Limbic areas of LeftLeg and RightLeg include the respective pelvis motors, given the 'average walking' model.");
            Console.ReadLine();
            Console.WriteLine("Capturing pose..." + Environment.NewLine);
            Logging.WriteLog("Captured pose:" + Environment.NewLine, Logging.LogType.Data, Logging.LogCaller.JoiPose);
        repeat:
            var all = MotorSequenceAll.ReturnDictionaryOfPositions(Limbic.All);
            var tableAll = new ConsoleTable("motor", "position");
            foreach (var kvp in all)
            {
                tableAll.AddRow(kvp.Key, kvp.Value);
            }
            tableAll.Write();
            Console.WriteLine();
            Logging.WriteLog(tableAll.ToString(), Logging.LogType.Data, Logging.LogCaller.JoiPose, OutputFileName);
            // Program help.
            Console.WriteLine("If wanting to rescan of everything, type 'do'.");
            Console.WriteLine("If wanting to rescan just a specific limbic region, type 'scan' and one of the supported areas: Abdomen, Bust, LeftArm, RightArm, LeftLeg, RightLeg.");
            Console.WriteLine("If wanting to freeze the pose-area, type 'freeze' and one of the supported areas: Abdomen, Bust, LeftArm, RightArm, LeftLeg, RightLeg.");
            Console.WriteLine("If wanting to unfreeze the pose-area, type 'unfreeze' and one of the supported areas: Abdomen, Bust, LeftArm, RightArm, LeftLeg, RightLeg.");
            Console.WriteLine("Otherwise, hit Enter to exit.");

            var input = Console.ReadLine();
            if (input == "do")
                goto repeat;
            switch (input)
            {
                case "scan Abdomen":
                    await Scan(Limbic.LimbicArea.Abdomen);
                    break;
                case "scan Bust":
                    await Scan(Limbic.LimbicArea.Bust);
                    break;
                case "scan LeftArm":
                    await Scan(Limbic.LimbicArea.LeftArm);
                    break;
                case "scan RightArm":
                    await Scan(Limbic.LimbicArea.RightArm);
                    break;
                case "scan LeftLeg":
                    await Scan(Limbic.LimbicArea.LeftLeg);
                    break;
                case "scan RightLeg":
                    await Scan(Limbic.LimbicArea.RightLeg);
                    break;
                case "freeze Abdomen":
                    await Freeze(Limbic.LimbicArea.Abdomen);
                    break;
                case "freeze Bust":
                    await Freeze(Limbic.LimbicArea.Bust);
                    break;
                case "freeze LeftArm":
                    await Freeze(Limbic.LimbicArea.LeftArm);
                    break;
                case "freeze RightArm":
                    await Freeze(Limbic.LimbicArea.RightArm);
                    break;
                case "freeze LeftLeg":
                    await Freeze(Limbic.LimbicArea.LeftLeg);
                    break;
                case "freeze RightLeg":
                    await Freeze(Limbic.LimbicArea.RightLeg);
                    break;
                case "unfreeze Abdomen":
                    await Unfreeze(Limbic.LimbicArea.Abdomen);
                    break;
                case "unfreeze Bust":
                    await Unfreeze(Limbic.LimbicArea.Bust);
                    break;
                case "unfreeze LeftArm":
                    await Unfreeze(Limbic.LimbicArea.LeftArm);
                    break;
                case "unfreeze RightArm":
                    await Unfreeze(Limbic.LimbicArea.RightArm);
                    break;
                case "unfreeze LeftLeg":
                    await Unfreeze(Limbic.LimbicArea.LeftLeg);
                    break;
                case "unfreeze RightLeg":
                    await Unfreeze(Limbic.LimbicArea.RightLeg);
                    break;
                default:
                    break;
            }
            Console.WriteLine("Pose capture complete.");
            Console.WriteLine("Disposing motors...");
        }
        
        MotorFunctions.DisposeDynamixelMotors();
        await Task.CompletedTask;
    }
#if windows
    static void SapiWindowsSpeechRecognized(object sender, SpeechRecognizedEventArgs e)
    {
        switch (e.Result.Text)
        {
            case "do":
                break;
            case "scan Abdomen":
                Scan(Limbic.LimbicArea.Abdomen);
                SpeakText("Abdomen scanned.");
                break;
            case "scan Bust":
                Scan(Limbic.LimbicArea.Bust);
                SpeakText("Bust scanned.");
                break;
            case "scan LeftArm":
                Scan(Limbic.LimbicArea.LeftArm);
                SpeakText("Left arm scanned.");
                break;
            case "scan RightArm":
                Scan(Limbic.LimbicArea.RightArm);
                SpeakText("Right arm scanned.");
                break;
            case "scan LeftLeg":
                Scan(Limbic.LimbicArea.LeftLeg);
                SpeakText("Left leg scanned.");
                break;
            case "scan RightLeg":
                Scan(Limbic.LimbicArea.RightLeg);
                SpeakText("Right leg scanned.");
                break;
            case "freeze Abdomen":
                Freeze(Limbic.LimbicArea.Abdomen);
                Console.WriteLine("Abdomen frozen");
                SpeakText("I have frozen the Abdomen.");
                break;
            case "freeze Bust":
                Freeze(Limbic.LimbicArea.Bust);
                Console.WriteLine("Bust frozen");
                SpeakText("I have frozen the Bust.");
                break;
            case "freeze LeftArm":
                Freeze(Limbic.LimbicArea.LeftArm);
                Console.WriteLine("Left arm frozen");
                SpeakText("I have frozen the left arm.");
                break;
            case "freeze RightArm":
                Freeze(Limbic.LimbicArea.RightArm);
                Console.WriteLine("Right arm frozen");
                SpeakText("I have frozen the right arm.");
                break;
            case "freeze LeftLeg":
                Freeze(Limbic.LimbicArea.LeftLeg);
                Console.WriteLine("Left leg frozen");
                SpeakText("I have frozen the left leg.");
                break;
            case "freeze RightLeg":
                Freeze(Limbic.LimbicArea.RightLeg);
                Console.WriteLine("Right leg frozen");
                SpeakText("I have frozen the right leg.");
                break;
            case "unfreeze Abdomen":
                Unfreeze(Limbic.LimbicArea.Abdomen);
                Console.WriteLine("Abdomen unfrozen");
                SpeakText("I have unfrozen the abdomen.");
                break;
            case "unfreeze Bust":
                Unfreeze(Limbic.LimbicArea.Bust);
                Console.WriteLine("Bust unfrozen");
                SpeakText("I have unfrozen the bust.");
                break;
            case "unfreeze LeftArm":
                Unfreeze(Limbic.LimbicArea.LeftArm);
                Console.WriteLine("Left arm unfrozen");
                SpeakText("I have unfrozen the left arm.");
                break;
            case "unfreeze RightArm":
                Unfreeze(Limbic.LimbicArea.RightArm);
                Console.WriteLine("Right arm unfrozen");
                SpeakText("I have unfrozen the right arm.");
                break;
            case "unfreeze LeftLeg":
                Unfreeze(Limbic.LimbicArea.LeftLeg);
                Console.WriteLine("Left leg unfrozen");
                SpeakText("I have unfrozen the left leg.");
                break;
            case "unfreeze RightLeg":
                Unfreeze(Limbic.LimbicArea.RightLeg);
                Console.WriteLine("Right leg unfrozen");
                SpeakText("I have unfrozen the right leg.");
                break;
            case "give me a list of what I can do":
                Console.WriteLine("You can scan, freeze, or unfreeze the jointed areas of the joi robot.");
                SpeakText("You can scan, freeze, or unfreeze the jointed areas of the joi robot.");
                break;
            case "what are the areas of the robot":
                Console.WriteLine("The supported areas are the abdomen, bust, left arm, right arm, left leg, and right leg on the joi robot.");
                SpeakText("The supported areas are the abdomen, bust, left arm, right arm, left leg, and right leg on the joi robot.");
                break;
            case "program quit":
                Console.WriteLine("Detected quit. Closing the application.");
                SpeakText("Detected quit. Closing the application.");
                MotorFunctions.DisposeDynamixelMotors();
                Environment.Exit(0);
                break;
            default:
                break;
        }
    }
#endif
}
