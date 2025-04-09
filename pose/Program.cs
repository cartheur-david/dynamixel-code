//
// This autonomous intelligent system software is the property of Cartheur Research B.V. Copyright 2021 - 2025, all rights reserved.
//
using Cartheur.Animals.Robot;
using ConsoleTables;

class PoseReader
{
    public static MotorFunctions MotorControl { get; set; }
    public static MotorSequence MotorSequenceAbdomen { get; set; }
    public static MotorSequence MotorSequenceLeftLeg { get; set; }
    public static MotorSequence MotorSequenceRightLeg { get; set; }
    public static bool MotorsInitialized { get; set; }

    static async Task Main()
    {
        MotorControl = new MotorFunctions();
        MotorSequenceAbdomen = new MotorSequence();
        MotorSequenceLeftLeg = new MotorSequence();
        MotorSequenceRightLeg = new MotorSequence();
        Console.WriteLine(MotorFunctions.InitializeDynamixelMotors());
        MotorsInitialized = MotorFunctions.DynamixelMotorsInitialized;
        MotorFunctions.CollateMotorArray();

        if (MotorsInitialized) { MotorControl.CreateConnectMotorObjects(); }
        else Logging.WriteLog("Cannot create connection objects.", Logging.LogType.Error, Logging.LogCaller.MotorControl);

        Console.WriteLine("Hold the robot in the standing pose, then press Enter...");
        Console.ReadLine(); // Wait for you to position it.

        // Read and display current positions.
        Console.WriteLine("\nSitting pose:" + Environment.NewLine);
        Logging.WriteLog("Sitting pose:" + Environment.NewLine, Logging.LogType.Data, Logging.LogCaller.JoiPose);
    repeat:
        var abdomen = MotorSequenceAbdomen.ReturnDictionaryOfPositions(Limbic.Abdomen);
        var tableAbdomen = new ConsoleTable("abdomen-motor", "position");
        foreach (var kvpa in abdomen)
        {
            tableAbdomen.AddRow(kvpa.Key, kvpa.Value);
        }
        tableAbdomen.Write();
        Console.WriteLine();
        Logging.WriteLog(tableAbdomen.ToString(), Logging.LogType.Data, Logging.LogCaller.JoiPose);

        var leftLeg = MotorSequenceLeftLeg.ReturnDictionaryOfPositions(Limbic.LeftLeg);
        var tableLeftLeg = new ConsoleTable("left-leg-motor", "position");
        foreach (var kvpl in leftLeg)
        {
            tableLeftLeg.AddRow(kvpl.Key, kvpl.Value);
        }
        tableLeftLeg.Write();
        Console.WriteLine();
        Logging.WriteLog(tableLeftLeg.ToString(), Logging.LogType.Data, Logging.LogCaller.JoiPose);

        var rightLeg = MotorSequenceRightLeg.ReturnDictionaryOfPositions(Limbic.RightLeg);
        var tablerightLeg = new ConsoleTable("right-leg-motor", "position");
        foreach (var kvpr in rightLeg)
        {
            tablerightLeg.AddRow(kvpr.Key, kvpr.Value);
        }
        tablerightLeg.Write();
        Console.WriteLine();
        Logging.WriteLog(tablerightLeg.ToString(), Logging.LogType.Data, Logging.LogCaller.JoiPose);

        Console.WriteLine("If wanting to rescan, type 'do'.");
        var input = Console.ReadLine(); // Wait for you to view the position values.
        if (input == "do")
            goto repeat;

        // Dispose resources.
        MotorFunctions.DisposeDynamixelMotors();

        await Task.CompletedTask;
    }
}
