using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Kinect;
using Microsoft.Xna.Framework.Input;
using Kandou_v1;

using AvatarKinectGame.TEST;

namespace AvatarKinectGame.GameComponents
{
    public class KinectSkinnedModelAnimation : DrawableGameComponent
    {
        private Model currentModel;

        Label label2;
        Label label3;

        private Avatar animator;

        private readonly Kinect chooser;

        private ColorStreamRenderer colorStream;

        private SkinnedEffect effect;

        private Dictionary<JointType, int> nuiJointToAvatarBoneIndex;

        private static readonly Vector3 SkeletonTranslationScaleFactor = new Vector3(40.0f, 40.0f, 40.0f);
        //

        private bool skeletonDetected;
        private bool kinectDetected;

        private float avatarHipCenterDrawHeight;

        bool avatarFront = true;

        bool colorStreamON = true;

        public KinectSkinnedModelAnimation(Game game)
            : base(game)
        {
            this.chooser = new Kinect(Game, ColorImageFormat.RgbResolution640x480Fps30, DepthImageFormat.Resolution320x240Fps30);
            Game.Services.AddService(typeof(Kinect), this.chooser);
            Game.Components.Add(this.chooser);

            this.animator = new Avatar(Game, this.RetargetMatrixHierarchyToAvatarMesh, SkeletonTranslationScaleFactor);
            Game.Components.Add(this.animator);

            //
            this.skeletonDetected = false;
            this.avatarHipCenterDrawHeight = 0.76f; //65
            //

            this.label2 = new Label(Game, new Vector2(5.0f, 25.0f));
            Game.Components.Add(this.label2);

            this.label3 = new Label(Game, new Vector2(5.0f, 45.0f));
            Game.Components.Add(this.label3);
        }

        public Kinect Chooser
        {
            get
            {
                return (Kinect)Game.Services.GetService(typeof(Kinect));
            }
        }

        public SpriteBatch spriteBatch
        {
            get
            {
                return (SpriteBatch)this.Game.Services.GetService(typeof(SpriteBatch));
            }
        }

        private static Skeleton[] SkeletonData { get; set; }


        protected void BuildJointHierarchy()
        {
            if (null == this.nuiJointToAvatarBoneIndex)
            {
                this.nuiJointToAvatarBoneIndex = new Dictionary<JointType, int>();
            }

            this.nuiJointToAvatarBoneIndex.Add(JointType.HipCenter, 0); // 0
            this.nuiJointToAvatarBoneIndex.Add(JointType.Spine, 4); // 4
            this.nuiJointToAvatarBoneIndex.Add(JointType.ShoulderCenter, 5); // 5
            this.nuiJointToAvatarBoneIndex.Add(JointType.Head, 7); // 7
            this.nuiJointToAvatarBoneIndex.Add(JointType.ElbowLeft, 14); // 14
            this.nuiJointToAvatarBoneIndex.Add(JointType.WristLeft, 15); // 15
            this.nuiJointToAvatarBoneIndex.Add(JointType.HandLeft, 16); // 16
            this.nuiJointToAvatarBoneIndex.Add(JointType.ElbowRight, 10); // 10
            this.nuiJointToAvatarBoneIndex.Add(JointType.WristRight, 11); // 11
            this.nuiJointToAvatarBoneIndex.Add(JointType.HandRight, 12); // 12
            this.nuiJointToAvatarBoneIndex.Add(JointType.KneeLeft, 22); // 22
            this.nuiJointToAvatarBoneIndex.Add(JointType.AnkleLeft, 23); // 23
            this.nuiJointToAvatarBoneIndex.Add(JointType.FootLeft, 24); // 24
            this.nuiJointToAvatarBoneIndex.Add(JointType.KneeRight, 18); // 18
            this.nuiJointToAvatarBoneIndex.Add(JointType.AnkleRight, 19); // 19
            this.nuiJointToAvatarBoneIndex.Add(JointType.FootRight, 20); // 20
        }

        protected override void LoadContent()
        {
            this.colorStream = new ColorStreamRenderer(Game, new Vector2(GraphicsDevice.Viewport.Width - 5 - 160, 5),
                new Vector2(160, 100));

            this.currentModel = Game.Content.Load<Model>("MODELS/ninja2/ninja/ninja2");
            if (null == this.currentModel)
            {
                throw new InvalidOperationException("Cannot load 3D avatar model");
            }
            
            effect = new SkinnedEffect(GraphicsDevice);

            effect.Texture = Game.Content.Load<Texture2D>("MODELS/ninja2/ninja/texture");

            foreach (ModelMesh mesh in currentModel.Meshes)
            {
                foreach (ModelMeshPart part in mesh.MeshParts)
                {
                    part.Effect = effect;
                }
            }
            
            this.animator.AvatarModel = this.currentModel;
            this.animator.AvatarHipCenterHeight = this.avatarHipCenterDrawHeight;
            
            this.BuildJointHierarchy();

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (null == this.chooser || null == this.Chooser.Sensor || false == this.Chooser.Sensor.IsRunning || this.Chooser.Sensor.Status != KinectStatus.Connected)
            {
                kinectDetected = false;
                label2.message = kinectDetected.ToString();

                // sprawdzanie czy komunikacja z sensorem zachodzi, jeżeli nie - omiń późniejsze operacje pobierania danych typu "Skeleton"
            }
            else kinectDetected = true;

            label2.message = kinectDetected.ToString();

            bool newFrame = false;

            if (kinectDetected)
            {
                using (var skeletonFrame = this.Chooser.Sensor.SkeletonStream.OpenNextFrame(0))
                {
                    // pobierz kolejną "ramkę" danych dotyczących interfejsu SkeletalTracking
                    if (null != skeletonFrame)
                    {
                        // instrukcje zawarte w tych klamrach zostaną wykonane, jeżeli "ramka" danych została poprawnie pobrana
                        newFrame = true;

                        if (null == SkeletonData || SkeletonData.Length != skeletonFrame.SkeletonArrayLength)
                        {
                            SkeletonData = new Skeleton[skeletonFrame.SkeletonArrayLength];
                            // otwórz nową tablicę obiektów typu "Skeleton" - tyle obiektów znajdzie się w tablicy ile osób zostało wychwyconych przez urządzenie
                        }

                        skeletonFrame.CopySkeletonDataTo(SkeletonData);
                        // przekopiuj dane z "ramki" do utworzonej nowej tablicy obiektów typu "Skeleton"

                        Skeleton rawSkeleton =
                            (from s in SkeletonData
                             where s != null && s.TrackingState == SkeletonTrackingState.Tracked
                             select s).FirstOrDefault();
                        // ustaw do obiektu rawSkeleton tylko 1, wybrany "szkielet" z pośród wszystkich dostępnych podzczas wychwycania osób przez sensor -> wybierz pierwszy z brzegu lub "domyślny" szkielet
                        // ważne w sytuacji gdy apliakcja wymaga tylko jednej danej typu "Skeleton" (dane tylko o 1 użytkowniku stojącego przed sensorem)

                        if (null != this.animator)
                        {
                            if (null != rawSkeleton)
                            {
                                //
                                // w tym miejscu można zastosować uzyskane dane dla konkretnego obiektu obsługującego np. ruch modelu 3D
                                //
                                this.animator.CopySkeleton(rawSkeleton);

                                if (this.skeletonDetected == false)
                                {
                                    this.animator.Reset();
                                }

                                this.skeletonDetected = true;
                                this.animator.SkeletonVisible = true;
                            }
                            else
                            {
                                this.skeletonDetected = false;
                                this.animator.SkeletonVisible = false;
                            }
                        }
                    }
                }

                if (newFrame)
                {
                    //
                    // w tym miejscu mozna wykonać odpowiednie operacje opierając się na wiedzy, że "ramka" danych dotyczących "NUI Skeletal Tracking" została poprawnie pobrana
                    //

                    if (null != this.animator)
                    {
                        this.animator.SkeletonDrawn = false;
                    }

                    if (null != this.colorStream)
                    {
                        this.colorStream.Update(gameTime);
                    }
                }
            }

            animator.Update(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            this.animator.Draw(gameTime);

            label3.message = skeletonDetected.ToString();


            if (null != this.colorStream && colorStreamON)
            {
                this.colorStream.Draw(gameTime);
            }

            base.Draw(gameTime);
        }


        #region AvatarRetargeting

        private void RetargetMatrixHierarchyToAvatarMesh(Skeleton skeleton, Matrix bindRoot, Matrix[] boneTransforms)
        {
            if (null == skeleton)
            {
                return;
            }

            foreach (BoneOrientation bone in skeleton.BoneOrientations)
            {
                if (skeleton.Joints[bone.EndJoint].TrackingState == JointTrackingState.NotTracked)
                {
                    continue;
                }

                this.SetJointTransformation(bone, skeleton, bindRoot, ref boneTransforms);
            }

            //this.SetAvatarRootWorldPosition(skeleton, ref boneTransforms);
        }

        // NEW METHOD
        private void SetJointTransformation(BoneOrientation bone, Skeleton skeleton, Matrix bindRoot, ref Matrix[] boneTransforms)
        {
            if (bone.StartJoint == JointType.HipCenter && bone.EndJoint == JointType.HipCenter)
            {
                bindRoot.Translation = Vector3.Zero;
                Matrix invBindRoot = Matrix.Invert(bindRoot);

                Matrix hipOrientation = KinectHelper.Matrix4ToXNAMatrix(bone.HierarchicalRotation.Matrix);

                Matrix pelvis = boneTransforms[0];
                pelvis.Translation = Vector3.Zero;
                Matrix invPelvis = Matrix.Invert(pelvis);

                Matrix combined;
                if (avatarFront) combined = (invBindRoot * hipOrientation) * invPelvis;
                else combined = invBindRoot;

                this.ReplaceBoneMatrix(JointType.HipCenter, combined, true, ref boneTransforms);
            }
            else if (bone.EndJoint == JointType.ShoulderCenter)
            {
                if (skeleton.Joints[JointType.HipCenter].TrackingState == JointTrackingState.NotTracked)
                {
                    Matrix hipOrientation = KinectHelper.Matrix4ToXNAMatrix(bone.HierarchicalRotation.Matrix);

                    Quaternion kinectRotation = KinectHelper.DecomposeMatRot(hipOrientation);
                    Quaternion avatarRotation = new Quaternion(kinectRotation.Y, kinectRotation.Z, kinectRotation.X, kinectRotation.W);
                    Matrix combined = Matrix.CreateFromQuaternion(avatarRotation);

                    this.ReplaceBoneMatrix(JointType.HipCenter, combined, true, ref boneTransforms);
                }
            }
            else if (bone.EndJoint == JointType.Spine)
            {
                Matrix tempMat = KinectHelper.Matrix4ToXNAMatrix(bone.HierarchicalRotation.Matrix);

                Matrix adjustment = Matrix.CreateRotationX(MathHelper.ToRadians(30));
                tempMat *= adjustment;

                Quaternion kinectRotation = KinectHelper.DecomposeMatRot(tempMat);    // XYZ
                Quaternion avatarRotation = new Quaternion(kinectRotation.Y, kinectRotation.Z, kinectRotation.X, kinectRotation.W); // transform from Kinect to avatar coordinate system
                tempMat = Matrix.CreateFromQuaternion(avatarRotation);

                this.ReplaceBoneMatrix(bone.EndJoint, tempMat, false, ref boneTransforms);
            }
            else if (bone.EndJoint == JointType.Head)
            {
                Matrix tempMat = KinectHelper.Matrix4ToXNAMatrix(bone.HierarchicalRotation.Matrix);

                Quaternion kinectRotation = KinectHelper.DecomposeMatRot(tempMat);
                Quaternion avatarRotation = new Quaternion(kinectRotation.Y, kinectRotation.Z, kinectRotation.X, kinectRotation.W);
                tempMat = Matrix.CreateFromQuaternion(avatarRotation);

                this.ReplaceBoneMatrix(bone.EndJoint, tempMat, false, ref boneTransforms);
            }
            else if (bone.EndJoint == JointType.ElbowLeft || bone.EndJoint == JointType.WristLeft)
            {
                Matrix tempMat = KinectHelper.Matrix4ToXNAMatrix(bone.HierarchicalRotation.Matrix);

                if (bone.EndJoint == JointType.ElbowLeft)
                {
                    Matrix adjustment = Matrix.CreateRotationY(MathHelper.ToRadians(60));
                    tempMat *= adjustment;
                    Matrix adjustment2 = Matrix.CreateRotationZ(MathHelper.ToRadians(15)); //15
                    tempMat *= adjustment2;
                }

                Quaternion kinectRotation = KinectHelper.DecomposeMatRot(tempMat);    // XYZ
                Quaternion avatarRotation = new Quaternion(kinectRotation.Y, kinectRotation.Z, kinectRotation.X, kinectRotation.W);
                tempMat = Matrix.CreateFromQuaternion(avatarRotation);

                this.ReplaceBoneMatrix(bone.EndJoint, tempMat, false, ref boneTransforms);
            }
            else if (bone.EndJoint == JointType.HandLeft)
            {
                Matrix tempMat = KinectHelper.Matrix4ToXNAMatrix(bone.HierarchicalRotation.Matrix);

                Quaternion kinectRotation = KinectHelper.DecomposeMatRot(tempMat);    // XYZ
                Quaternion avatarRotation = new Quaternion(kinectRotation.Y, kinectRotation.X, kinectRotation.Z, kinectRotation.W);
                tempMat = Matrix.CreateFromQuaternion(avatarRotation);

                this.ReplaceBoneMatrix(bone.EndJoint, tempMat, false, ref boneTransforms);
            }
            else if (bone.EndJoint == JointType.ElbowRight || bone.EndJoint == JointType.WristRight)
            {
                Matrix tempMat = KinectHelper.Matrix4ToXNAMatrix(bone.HierarchicalRotation.Matrix);

                if (bone.EndJoint == JointType.ElbowRight)
                {
                    Matrix adjustment = Matrix.CreateRotationY(MathHelper.ToRadians(-60));
                    tempMat *= adjustment;
                    Matrix adjustment2 = Matrix.CreateRotationZ(MathHelper.ToRadians(-15)); //-15
                    tempMat *= adjustment2;
                }

                Quaternion kinectRotation = KinectHelper.DecomposeMatRot(tempMat);    // XYZ
                Quaternion avatarRotation = new Quaternion(kinectRotation.Y, kinectRotation.Z, kinectRotation.X, kinectRotation.W);
                tempMat = Matrix.CreateFromQuaternion(avatarRotation);

                this.ReplaceBoneMatrix(bone.EndJoint, tempMat, false, ref boneTransforms);
            }
            else if (bone.EndJoint == JointType.HandRight)
            {
                Matrix tempMat = KinectHelper.Matrix4ToXNAMatrix(bone.HierarchicalRotation.Matrix);

                Quaternion kinectRotation = KinectHelper.DecomposeMatRot(tempMat);    // XYZ
                Quaternion avatarRotation = new Quaternion(kinectRotation.Y, kinectRotation.X, kinectRotation.Z, kinectRotation.W);
                tempMat = Matrix.CreateFromQuaternion(avatarRotation);

                this.ReplaceBoneMatrix(bone.EndJoint, tempMat, false, ref boneTransforms);
            }
            else if (bone.EndJoint == JointType.KneeLeft)
            {
                Matrix kneeLeft = KinectHelper.Matrix4ToXNAMatrix(bone.HierarchicalRotation.Matrix);
                Matrix combined = kneeLeft;

                Matrix adjustment = Matrix.CreateRotationZ(MathHelper.ToRadians(20));
                combined *= adjustment;
                adjustment = Matrix.CreateRotationY(MathHelper.ToRadians(10));
                combined *= adjustment;

                this.SetLegMatrix(bone.EndJoint, combined, ref boneTransforms);
            }
            else if (bone.EndJoint == JointType.AnkleLeft || bone.EndJoint == JointType.AnkleRight)
            {
                Matrix tempMat = KinectHelper.Matrix4ToXNAMatrix(bone.HierarchicalRotation.Matrix);

                if (bone.EndJoint == JointType.AnkleLeft)
                {
                    Matrix adjustment = Matrix.CreateRotationZ(MathHelper.ToRadians(-90));
                    tempMat *= adjustment;
                    adjustment = Matrix.CreateRotationY(MathHelper.ToRadians(10));
                    tempMat *= adjustment;
                }
                else
                {
                    Matrix adjustment = Matrix.CreateRotationZ(MathHelper.ToRadians(90));
                    tempMat *= adjustment;
                    adjustment = Matrix.CreateRotationY(MathHelper.ToRadians(-10));
                    tempMat *= adjustment;
                }

                Quaternion kinectRotation = KinectHelper.DecomposeMatRot(tempMat);  // XYZ
                Quaternion avatarRotation = new Quaternion(kinectRotation.Y, kinectRotation.Z, kinectRotation.X , kinectRotation.W);
                tempMat = Matrix.CreateFromQuaternion(avatarRotation);

                this.ReplaceBoneMatrix(bone.EndJoint, tempMat, false, ref boneTransforms);
            }
            else if (bone.EndJoint == JointType.KneeRight)
            {
                Matrix kneeRight = KinectHelper.Matrix4ToXNAMatrix(bone.HierarchicalRotation.Matrix);
                Matrix combined = kneeRight;

                Matrix adjustment = Matrix.CreateRotationZ(MathHelper.ToRadians(-20));
                combined *= adjustment;
                adjustment = Matrix.CreateRotationY(MathHelper.ToRadians(-10));
                combined *= adjustment;

                this.SetLegMatrix(bone.EndJoint, combined, ref boneTransforms);
            }
        }

        private void SetLegMatrix(JointType joint, Matrix legRotation, ref Matrix[] boneTransforms)
        {
            Quaternion kinectRotation = KinectHelper.DecomposeMatRot(legRotation);  // XYZ
            Quaternion avatarRotation = new Quaternion(kinectRotation.Y, kinectRotation.Z, kinectRotation.X, kinectRotation.W);
            legRotation = Matrix.CreateFromQuaternion(avatarRotation);

            this.ReplaceBoneMatrix(joint, legRotation, false, ref boneTransforms);
        }

        private void ReplaceBoneMatrix(JointType joint, Matrix boneMatrix, bool replaceTranslationInExistingBoneMatrix, ref Matrix[] boneTransforms)
        {
            int meshJointId;
            bool success = this.nuiJointToAvatarBoneIndex.TryGetValue(joint, out meshJointId);

            if (success)
            {
                Vector3 offsetTranslation = boneTransforms[meshJointId].Translation;
                boneTransforms[meshJointId] = boneMatrix;

                if (replaceTranslationInExistingBoneMatrix == false)
                {
                    boneTransforms[meshJointId].Translation = offsetTranslation;
                }
            }
        }

        #endregion


        #region Helpers

        private void GraphicsDevicePreparingDeviceSettings(object sender, PreparingDeviceSettingsEventArgs e)
        {
            e.GraphicsDeviceInformation.PresentationParameters.RenderTargetUsage = RenderTargetUsage.PreserveContents;
        }

        #endregion
    }
}
