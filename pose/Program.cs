//
// This autonomous intelligent system software is the property of Cartheur Research B.V. Copyright 2021 - 2025, all rights reserved.
//
using Cartheur.Animals.Robot;
using ConsoleTables;

class PoseReader
{
    public static MotorFunctions MotorControl { get; set; }
    public static MotorSequence MotorSequenceAbdomen { get; set; }
    public static MotorSequence MotorSequenceBust { get; set; }
    public static MotorSequence MotorSequenceLeftArm { get; set; }
    public static MotorSequence MotorSequenceLeftLeg { get; set; }
    public static MotorSequence MotorSequenceLeftPelvis { get; set; }
    public static MotorSequence MotorSequenceRightArm { get; set; }
    public static MotorSequence MotorSequenceRightLeg { get; set; }
    public static MotorSequence MotorSequenceRightPelvis { get; set; }
    public static bool MotorsInitialized { get; set; }

    static async Task Main()
    {
        MotorControl = new MotorFunctions();
        MotorSequenceAbdomen = new MotorSequence();
        MotorSequenceBust = new MotorSequence();
        MotorSequenceLeftArm = new MotorSequence();
        MotorSequenceLeftLeg = new MotorSequence();
        MotorSequenceLeftPelvis = new MotorSequence();
        MotorSequenceRightArm = new MotorSequence();
        MotorSequenceRightLeg = new MotorSequence();
        MotorSequenceRightPelvis = new MotorSequence();
        Console.WriteLine(MotorFunctions.InitializeDynamixelMotors());
        MotorsInitialized = MotorFunctions.DynamixelMotorsInitialized;
        MotorFunctions.CollateMotorArray();

        if (MotorsInitialized) { MotorControl.CreateConnectMotorObjects(); }
        else Logging.WriteLog("Cannot create connection objects.", Logging.LogType.Error, Logging.LogCaller.MotorControl);

        Console.WriteLine("Hold the robot in the desired pose for motor capture, then press Enter...");
        Console.ReadLine(); // Wait for you to position it.

        // Read and display current positions.
        Console.WriteLine("\nSitting pose:" + Environment.NewLine);
        Logging.WriteLog("Sitting pose:" + Environment.NewLine, Logging.LogType.Data, Logging.LogCaller.JoiPose);
    // Bust, Head, LeftArm, RightArm, LeftPelvis, RightPelvis, 
    repeat:
        // Abdomen.
        var abdomen = MotorSequenceAbdomen.ReturnDictionaryOfPositions(Limbic.Abdomen);
        var tableAbdomen = new ConsoleTable("abdomen", "position");
        foreach (var kvpa in abdomen)
        {
            tableAbdomen.AddRow(kvpa.Key, kvpa.Value);
        }
        tableAbdomen.Write();
        Console.WriteLine();
        Logging.WriteLog(tableAbdomen.ToString(), Logging.LogType.Data, Logging.LogCaller.JoiPose);
        // Bust.
        var bust = MotorSequenceBust.ReturnDictionaryOfPositions(Limbic.Bust);
        var tableBust = new ConsoleTable("bust", "position");
        foreach (var kvpb in bust)
        {
            tableBust.AddRow(kvpb.Key, kvpb.Value);
        }
        tableBust.Write();
        Console.WriteLine();
        Logging.WriteLog(tableBust.ToString(), Logging.LogType.Data, Logging.LogCaller.JoiPose);
        // Left arm.
        var leftArm = MotorSequenceLeftArm.ReturnDictionaryOfPositions(Limbic.LeftArm);
        var tableLeftArm = new ConsoleTable("leftarm", "position");
        foreach (var kvpla in leftArm)
        {
            tableLeftArm.AddRow(kvpla.Key, kvpla.Value);
        }
        tableLeftArm.Write();
        Console.WriteLine();
        Logging.WriteLog(tableLeftArm.ToString(), Logging.LogType.Data, Logging.LogCaller.JoiPose);
        // Left leg.
        var leftLeg = MotorSequenceLeftLeg.ReturnDictionaryOfPositions(Limbic.LeftLeg);
        var tableLeftLeg = new ConsoleTable("leftleg", "position");
        foreach (var kvpl in leftLeg)
        {
            tableLeftLeg.AddRow(kvpl.Key, kvpl.Value);
        }
        tableLeftLeg.Write();
        Console.WriteLine();
        Logging.WriteLog(tableLeftLeg.ToString(), Logging.LogType.Data, Logging.LogCaller.JoiPose);
        // Left pelvis.
        var leftPelvis = MotorSequenceLeftPelvis.ReturnDictionaryOfPositions(Limbic.LeftPelvis);
        var tableLeftPelvis = new ConsoleTable("leftpelvis", "position");
        foreach (var kvplp in leftPelvis)
        {
            tableLeftPelvis.AddRow(kvplp.Key, kvplp.Value);
        }
        tableLeftPelvis.Write();
        Console.WriteLine();
        Logging.WriteLog(tableLeftPelvis.ToString(), Logging.LogType.Data, Logging.LogCaller.JoiPose);
        // Right arm.
        var rightArm = MotorSequenceRightArm.ReturnDictionaryOfPositions(Limbic.RightArm);
        var tablerightArm = new ConsoleTable("rightarm", "position");
        foreach (var kvpr in rightArm)
        {
            tablerightArm.AddRow(kvpr.Key, kvpr.Value);
        }
        tablerightArm.Write();
        Console.WriteLine();
        Logging.WriteLog(tablerightArm.ToString(), Logging.LogType.Data, Logging.LogCaller.JoiPose);
        // Right leg.
        var rightLeg = MotorSequenceRightLeg.ReturnDictionaryOfPositions(Limbic.RightLeg);
        var tablerightLeg = new ConsoleTable("rightleg", "position");
        foreach (var kvpr in rightLeg)
        {
            tablerightLeg.AddRow(kvpr.Key, kvpr.Value);
        }
        tablerightLeg.Write();
        Console.WriteLine();
        Logging.WriteLog(tablerightLeg.ToString(), Logging.LogType.Data, Logging.LogCaller.JoiPose);
        // Right pelvis.
        var rightPelvis = MotorSequenceRightPelvis.ReturnDictionaryOfPositions(Limbic.RightPelvis);
        var tablerightPelvis = new ConsoleTable("rightpelvis", "position");
        foreach (var kvpr in rightPelvis)
        {
            tablerightPelvis.AddRow(kvpr.Key, kvpr.Value);
        }
        tablerightPelvis.Write();
        Console.WriteLine();
        Logging.WriteLog(tablerightPelvis.ToString(), Logging.LogType.Data, Logging.LogCaller.JoiPose);

        Console.WriteLine("If wanting to rescan, type 'do', otherwise Enter to exit.");
        var input = Console.ReadLine(); // Wait for you to view the position values.
        if (input == "do")
            goto repeat;

        // Dispose resources.
        MotorFunctions.DisposeDynamixelMotors();

        await Task.CompletedTask;
    }
}
