using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Physics
{
    public class Controller
    {
        private float moveSpeed = 1.5f;
        private float sens = 0.005f;

        private Vector3 position = new Vector3(0.0f, 0.0f, 3.0f);
        private Vector3 forward = new Vector3(0.0f, 0.0f, -1.0f);
        private Vector3 top = new Vector3(0.0f, 1.0f, 0.0f);
        private Vector3 right;

        private float pitch;
        private float yaw;

        public Matrix4 View { get; private set; }

        public Controller()
        {
            right = Vector3.Normalize(Vector3.Cross(forward, top));
        }

        public void LookAt(Vector3 target)
        {
            forward.X = (float)Math.Cos(pitch) * (float)Math.Cos(yaw);
            forward.Z = (float)Math.Cos(pitch) * (float)Math.Sin(yaw);
            forward.Y = (float)Math.Sin(pitch);

            forward = forward.Normalized();

            View = Matrix4.LookAt(position, position + forward, top);
        }

        public void UpdatePosition(KeyboardState input, float deltaTime)
        {
            if (input.IsKeyDown(Keys.D))
            {
                position += right * moveSpeed * deltaTime;
            }

            if (input.IsKeyDown(Keys.A))
            {
                position += -right * moveSpeed * deltaTime;
            }

            if (input.IsKeyDown(Keys.W))
            {
                position += forward * moveSpeed * deltaTime;
            }

            if (input.IsKeyDown(Keys.S))
            {
                position += -forward * moveSpeed * deltaTime;
            }

            if (input.IsKeyDown(Keys.Space))
            {
                position += top * moveSpeed * deltaTime; 
            }

            if (input.IsKeyDown(Keys.LeftShift))
            {
                position += -top * moveSpeed * deltaTime;
            }
        }

        public void UpdateView(Vector2 mouseChange)
        {
            yaw += mouseChange.X * sens;
            pitch -= mouseChange.Y * sens;

            if (pitch > 89.0f)
            {
                pitch = 89.0f;
            }
            else if (pitch < -89.0f)
            {
                pitch = -89.0f;
            }
        }
    }
}
