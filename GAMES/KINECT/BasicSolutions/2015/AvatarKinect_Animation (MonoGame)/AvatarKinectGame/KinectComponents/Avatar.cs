
namespace AvatarKinectGame.GameComponents
{
    using System;
    using Microsoft.Kinect;
    using Kandou_v1.Filters;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using SkinnedModelProcessor;
    using Kandou_v1;
    using AvatarKinectGame.TEST;

    public delegate void RetargetMatrixHierarchyToAvatarMesh(Skeleton skeleton, Matrix bindRoot, Matrix[] boneTransforms);


    public class Avatar : DrawableGameComponent
    {
        private readonly RetargetMatrixHierarchyToAvatarMesh retargetMethod;

        private Skeleton skeleton;

        private KeyboardState previousKeyboard;

        private Model currentModel;

        private Matrix[] boneTransforms;
        private Matrix[] worldTransforms;
        private Matrix[] skinTransforms;

        private SkinningData skinningDataValue;


        private bool useKinectAvateering;
        private bool mirrorView;

        
        private SkeletonJointsPositionDoubleExponentialFilter jointPositionFilter;

        private BoneOrientationDoubleExponentialFilter boneOrientationFilter;

        private RasterizerState rasterizerState;
        private bool initialized;

        public float rotationStartY = 180.0f;
        public float rotationStartX = 0.0f;

        private Label label3;

        private Vector3 SkeletonTranslationScaleFactor { get; set; }

        public Avatar(Game game, RetargetMatrixHierarchyToAvatarMesh retarget, Vector3 skeletonTranslationScaleFactor)
            : base(game)
        {
            if (null == game)
            {
                return;
            }
            this.SkeletonTranslationScaleFactor = skeletonTranslationScaleFactor;

            label3 = new Label(game, new Vector2(5.0f, 65.0f));
            Game.Components.Add(label3);

            this.retargetMethod = retarget;
            this.SkeletonDrawn = true;
            this.useKinectAvateering = true;

            this.jointPositionFilter = new Kandou_v1.Filters.SkeletonJointsPositionDoubleExponentialFilter();
            this.boneOrientationFilter = new Kandou_v1.Filters.BoneOrientationDoubleExponentialFilter();

            this.mirrorView = true;

            var jointPositionSmoothParameters = new TransformSmoothParameters
            {
                Smoothing = 0.25f,
                Correction = 0.25f,
                Prediction = 0.75f,
                JitterRadius = 0.1f,
                MaxDeviationRadius = 0.04f
            };

            this.jointPositionFilter.Init(jointPositionSmoothParameters);

            var boneOrientationSmoothparameters = new TransformSmoothParameters
            {
                Smoothing = 0.5f,
                Correction = 0.8f,
                Prediction = 0.75f,
                JitterRadius = 0.1f,
                MaxDeviationRadius = 0.1f
            };

            this.boneOrientationFilter.Init(boneOrientationSmoothparameters);
        }


        public bool SkeletonDrawn { get; set; }
        public bool SkeletonVisible { get; set; }

        public Skeleton RawSkeleton { get; set; }

        public float AvatarHipCenterHeight { get; set; }


        public Kinect Chooser
        {
            get
            {
                return (Kinect)Game.Services.GetService(typeof(Kinect));
            }
        }


        public Model AvatarModel
        {
            get
            {
                return this.currentModel;
            }

            set
            {
                if (value == null)
                {
                    return;
                }

                this.currentModel = value;

                SkinningData skinningData = this.currentModel.Tag as SkinningData;
                if (null == skinningData)
                {
                    throw new InvalidOperationException("This model does not contain a Skinning Data tag.");
                }

                this.skinningDataValue = skinningData;

                this.boneTransforms = new Matrix[skinningData.BindPose.Count];
                this.worldTransforms = new Matrix[skinningData.BindPose.Count];
                this.skinTransforms = new Matrix[skinningData.BindPose.Count];

                this.skinningDataValue.BindPose.CopyTo(this.boneTransforms, 0);
                this.UpdateWorldTransforms(Matrix.Identity);
                this.UpdateSkinTransforms();
            }
        }

        public void Reset()
        {
            if (null != this.jointPositionFilter)
            {
                this.jointPositionFilter.Reset();
            }

            if (null != this.boneOrientationFilter)
            {
                this.boneOrientationFilter.Reset();
            }
        }

        public void CopySkeleton(Skeleton sourceSkeleton)
        {
            if (null == sourceSkeleton)
            {
                return;
            }

            if (null == this.skeleton)
            {
                this.skeleton = new Skeleton();
            }

            KinectHelper.CopySkeleton(sourceSkeleton, this.skeleton);
        }

        public override void Initialize()
        {
            base.Initialize();
            this.rasterizerState = new RasterizerState();
            if (null != this.rasterizerState)
            {
                this.rasterizerState.ScissorTestEnable = true;
            }

            this.initialized = true;
        }

        public void Update(GameTime gameTime)
        {
            this.HandleInput();

            if (null == this.Chooser || null == this.Chooser.Sensor || false == this.Chooser.Sensor.IsRunning || this.Chooser.Sensor.Status != KinectStatus.Connected)
            {
                return;
            }

            if (false == this.SkeletonDrawn && null != this.skeleton && this.useKinectAvateering && this.SkeletonVisible)
            {
                this.skinningDataValue.BindPose.CopyTo(this.boneTransforms, 0);
                this.UpdateWorldTransforms(Matrix.Identity);
                this.UpdateSkinTransforms();

                if (this.mirrorView)
                {
                    SkeletonJointsMirror.MirrorSkeleton(this.skeleton);
                }

                this.jointPositionFilter.UpdateFilter(this.skeleton);

                this.boneOrientationFilter.UpdateFilter(this.skeleton);

                if (null != this.retargetMethod)
                {
                    this.retargetMethod(this.skeleton, this.skinningDataValue.BindPose[0], this.boneTransforms);
                }

                this.UpdateWorldTransforms(Matrix.Identity);

                this.UpdateSkinTransforms();
            }

            base.Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            if (false == this.initialized)
            {
                this.Initialize();
            }
            Rectangle oldScissorRectangle = GraphicsDevice.ScissorRectangle;
            GraphicsDevice.BlendState = BlendState.AlphaBlend;
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            GraphicsDevice.ScissorRectangle = oldScissorRectangle;

            Matrix view = Matrix.CreateTranslation(0, -4.5f, -15);// *
            //Matrix.CreateRotationY(MathHelper.ToRadians(obrot));
            //Matrix.CreateRotationX(MathHelper.ToRadians(obrot)); //*
            //Matrix.CreateLookAt(new Vector3(0, 0, -40), new Vector3(0, 0, 0), Vector3.Up);

            Matrix projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4,
                                                                    Game.GraphicsDevice.Viewport.AspectRatio,
                                                                    1,
                                                                    10000);

            foreach (ModelMesh mesh in this.currentModel.Meshes)
            {
                foreach (SkinnedEffect effect in mesh.Effects)
                {
                    effect.View = view;
                    effect.Projection = projection;

                    effect.EnableDefaultLighting();

                    effect.SpecularColor = new Vector3(0.35f);
                    effect.SpecularPower = 40;

                    effect.World =
                        Matrix.CreateRotationY(MathHelper.ToRadians(rotationStartY)) *
                        Matrix.CreateRotationX(MathHelper.ToRadians(rotationStartX));
                }

                mesh.Draw();
            }

            this.SkeletonDrawn = true;

            label3.message = "Rotacja startowa: " + rotationStartY.ToString();

            base.Draw(gameTime);
        }
        
        private void HandleInput()
        {
            KeyboardState currentKeyboard = Keyboard.GetState();

            if (currentKeyboard.IsKeyDown(Keys.Right))
            {
                rotationStartY += 5.0f;
            }
            
            if (currentKeyboard.IsKeyDown(Keys.Left))
            {
                rotationStartY -= 5.0f;
            }

            if (currentKeyboard.IsKeyDown(Keys.K))
            {
                if (!this.previousKeyboard.IsKeyDown(Keys.K))
                {
                    this.useKinectAvateering = !this.useKinectAvateering;
                }
            }

            this.previousKeyboard = currentKeyboard;
        }

        private void UpdateWorldTransforms(Matrix rootTransform)
        {
            this.worldTransforms[0] = this.boneTransforms[0] * rootTransform;

            for (int bone = 1; bone < this.worldTransforms.Length; bone++)
            {
                int parentBone = this.skinningDataValue.SkeletonHierarchy[bone];

                this.worldTransforms[bone] = this.boneTransforms[bone] * this.worldTransforms[parentBone];
            }
        }

        private void UpdateSkinTransforms()
        {
            for (int bone = 0; bone < this.skinTransforms.Length; bone++)
            {
                this.skinTransforms[bone] = this.skinningDataValue.InverseBindPose[bone] * this.worldTransforms[bone];
            }
        }
    }
}
