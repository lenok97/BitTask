using System;

namespace BitTask
{ 
	public class CameraParams
	{
		private float averageHumanHeight = 160;

        public CameraParams(float distance, float cameraHeight)
        {
            Distance = ValidateParam(distance);
            CameraHeight = ValidateParam(cameraHeight);
        }

        public long Id { get; set; }

		public float Distance { get; set; }

        public float CameraHeight { get; set; }

        public float B 
        { get => 
			Math.Abs(CameraHeight - averageHumanHeight); }

        public float Angle
		{ get => 
				(float)((180 / Math.PI) * Math.Atan(B / Distance));}

		private float ValidateParam(float param)
        {
			if (param <= 0)
				throw new ArgumentException("Parameter should be > 0");
			return param;
        }
	}
}
