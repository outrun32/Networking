using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace GameServer
{
    class Player
    {
        public int id;
        public string username;

        public Vector3 position;
        public Quaternion rotation;

        public Vector3 direction;

        private float moveSpeed = 20f / Constants.TICKS_PER_SEC;
        private float[] inputs;

        public Player(int _id, string _username, Vector3 _spawnPosition)
        {
            id = _id;
            username = _username;
            position = _spawnPosition;
            rotation = Quaternion.Identity;

            inputs = new float[2];
        }

        public void Update()
        {
            Vector2 floatInput = new Vector2(inputs[0], inputs[1]);
            Move(floatInput);
        }

        private void Move(Vector2 _inputDirection)
        {
            //TODO: Class Transform
            direction = ((MultiplyQV(rotation, new Vector3(0,0,1))) * _inputDirection.Y + (MultiplyQV(rotation, new Vector3(1, 0, 0))) * _inputDirection.X) * moveSpeed;

            ServerSend.PlayerDirection(this);
            ServerSend.PlayerRotation(this);
        }

        public void SetInput(float[] _inputs, Quaternion _rotation)
        {
            inputs = _inputs;
            rotation = _rotation;
        }
        //TODO: УБЕРИ ОТСЮДА ЭТО НАХУЙ
        public static Vector3 MultiplyQV(Quaternion rotation, Vector3 point)
        {
            float num1 = rotation.X * 2f;
            float num2 = rotation.Y * 2f;
            float num3 = rotation.Z * 2f;
            float num4 = rotation.X * num1;
            float num5 = rotation.Y * num2;
            float num6 = rotation.Z * num3;
            float num7 = rotation.X * num2;
            float num8 = rotation.Y * num3;
            float num9 = rotation.Y * num3;
            float num10 = rotation.W * num1;
            float num11 = rotation.W * num2;
            float num12 = rotation.W * num3;
            Vector3 vector3 = new Vector3();
            vector3.X = (float)((1.0 - ((double)num5 + (double)num6)) * (double)point.X + ((double)num7 - (double)num12) * (double)point.Y + ((double)num8 + (double)num11) * (double)point.Z);
            vector3.Y = (float)(((double)num7 + (double)num12) * (double)point.X + (1.0 - ((double)num4 + (double)num6)) * (double)point.Y + ((double)num9 - (double)num10) * (double)point.Z);
            vector3.Z = (float)(((double)num8 - (double)num11) * (double)point.X + ((double)num9 + (double)num10) * (double)point.Y + (1.0 - ((double)num4 + (double)num5)) * (double)point.Z);
            return vector3;
        }
    }
}
