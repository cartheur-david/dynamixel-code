using Cartheur.Animals.Robot;

class PoseReader
{
    public static MotorFunctions MotorControl { get; set; }
    public static bool MotorsInitialized { get; set; }

    static void Main(string[] args)
    {

        MotorControl = new MotorFunctions();
        var status = MotorControl.InitializeDynamixelMotors();
        MotorsInitialized = MotorFunctions.DynamixelMotorsInitialized;
        MotorFunctions.CollateMotorArray();

        if (MotorsInitialized) { MotorControl.CreateConnectMotorObjects(); }
        else Logging.WriteLog("Cannot create connection objects.", Logging.LogType.Error, Logging.LogCaller.MotorControl);
        
        var robot = new DynamixelController("COM3", 1000000); // Your port and baud rate
        robot.Connect();

        // List of motor IDs (adjust based on your robot)
        int[] motorIds = { 1, 2, 3, 4, 5, 6 }; // e.g., left hip, knee, ankle; right hip, knee, ankle

        Console.WriteLine("Hold the robot in the standing pose, then press Enter...");
        Console.ReadLine(); // Wait for you to position it

        // Read and display current positions
        var pose = new Dictionary<int, int>();
        foreach (int id in motorIds)
        {
            int position = robot.GetPosition(id);
            pose[id] = position;
            Console.WriteLine($"Motor {id}: Position = {position}");
        }

        // Output as code-friendly format
        Console.WriteLine("\nStanding pose dictionary:");
        Console.Write("var standingPose = new Dictionary<int, int> { ");
        foreach (var kvp in pose)
        {
            Console.Write($"{{ {kvp.Key}, {kvp.Value} }}, ");
        }
        Console.WriteLine("};");

        robot.Disconnect();
    }
}