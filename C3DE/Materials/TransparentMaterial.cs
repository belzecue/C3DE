﻿using C3DE.Components.Renderers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace C3DE.Materials
{
    public class TransparentMaterial : Material
    {
        private Vector4 _emissiveColor;
        private Vector4 _transparentColor;

        public Color TransparentColor
        {
            get { return new Color(_transparentColor); }
            set { _transparentColor = value.ToVector4(); }
        }

        public Color EmissiveColor
        {
            get { return new Color(_emissiveColor); }
            set { _emissiveColor = value.ToVector4(); }
        }

        public TransparentMaterial(Scene scene)
            : base(scene)
        {
            diffuseColor = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
            _emissiveColor = new Vector4(0.0f, 0.0f, 0.0f, 1.0f);
            _transparentColor = new Vector4(1.0f, 0.0f, 1.0f, 1.0f);
        }

        public override void LoadContent(ContentManager content)
        {
            effect = content.Load<Effect>("FX/TransparentEffect").Clone();
        }

        public override void PrePass()
        {
            effect.Parameters["View"].SetValue(scene.MainCamera.view);
            effect.Parameters["Projection"].SetValue(scene.MainCamera.projection);

            // Material
            effect.Parameters["AmbientColor"].SetValue(scene.RenderSettings.ambientColor);
            effect.Parameters["DiffuseColor"].SetValue(diffuseColor);
            effect.Parameters["EmissiveColor"].SetValue(_emissiveColor);
            effect.Parameters["TransparentColor"].SetValue(_transparentColor);
        }

        public override void Pass(RenderableComponent renderable)
        {
            effect.Parameters["MainTexture"].SetValue(mainTexture);
            effect.Parameters["World"].SetValue(renderable.SceneObject.Transform.world);
            effect.CurrentTechnique.Passes[0].Apply();
        }
    }
}
