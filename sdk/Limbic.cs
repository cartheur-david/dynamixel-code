//
// This autonomous intelligent system software is the property of Cartheur Research B.V. Copyright 2021 - 2025, all rights reserved.
//
namespace Cartheur.Animals.Robot
{
    /// <summary>
    /// The collection of motors by limbic, or, analogous human limbs on the joi robot.
    /// </summary>
    /// <remarks>Pelvis is a part of the respective leg-limbic areas.</remarks>
    public static class Limbic
    {
        public enum LimbicArea { Abdomen, All, Bust, Head, LeftArm, RightArm, LeftPelvis, RightPelvis, LeftLeg, RightLeg, Arms, Legs }

        public static readonly string[] Abdomen = { "abs_y", "abs_x", "abs_z" };
        public static readonly string[] Bust = { "bust_y", "bust_x" };
        public static readonly string[] Head = { "head_z", "head_y" };
        public static readonly string[] LeftArm = { "l_shoulder_y", "l_shoulder_x", "l_arm_z", "l_elbow_y" };
        public static readonly string[] RightArm = { "r_shoulder_y", "r_shoulder_x", "r_arm_z", "r_elbow_y" };
        public static readonly string[] LeftLeg = { "l_hip_x", "l_hip_z", "l_hip_y", "l_knee_y", "l_ankle_y" };
        public static readonly string[] RightLeg = { "r_hip_x", "r_hip_z", "r_hip_y", "r_knee_y", "r_ankle_y" };
        // Separated hip motors for the control keypad application.
        public static readonly string[] LeftPelvis = { "l_hip_x" };
        public static readonly string[] RightPelvis = { "r_hip_x" };
        public static readonly string[] LeftLegNoPelvis = { "l_hip_z", "l_hip_y", "l_knee_y", "l_ankle_y" };
        public static readonly string[] RightLegNoPelvis = { "r_hip_z", "r_hip_y", "r_knee_y", "r_ankle_y" };
        public static readonly string[] LeftAnkle = { "l_ankle_y" };
        public static readonly string[] RightAnkle = { "r_ankle_y" };
        // Regions
        public static readonly string[] Arms = { "bust_y", "bust_x", "l_shoulder_y", "l_shoulder_x", "l_arm_z", "l_elbow_y", "r_shoulder_y", "r_shoulder_x", "r_arm_z", "r_elbow_y" };
        public static readonly string[] Legs = { "l_hip_x", "l_hip_z", "l_hip_y", "l_knee_y", "l_ankle_y", "r_hip_x", "r_hip_z", "r_hip_y", "r_knee_y", "r_ankle_y" };

        /// <summary>
        /// The full list of limbic declarations for the joi robot.
        /// </summary>
        /// <remarks>A full-set of twenty-five motors.</remarks>
        public static readonly string[] All =
        {
               "abs_y", "abs_x", "abs_z", "bust_y", "bust_x", "head_z", "head_y",
               "l_shoulder_y", "l_shoulder_x", "l_arm_z", "l_elbow_y",
               "r_shoulder_y", "r_shoulder_x", "r_arm_z", "r_elbow_y",
               "l_hip_z", "l_hip_y", "l_knee_y", "l_ankle_y", "l_hip_x",
               "r_hip_z", "r_hip_y", "r_knee_y", "r_ankle_y", "r_hip_x"
           };
    }
}
