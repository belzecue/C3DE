﻿using C3DE.VR;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace C3DE.Graphics.PostProcessing
{
    public class SimpleBlur : PostProcessPass
    {
        private Effect m_Effect;
        private RenderTarget2D m_SceneRenderTarget;

        public float BlurDistance { get; set; } = 0;

        public SimpleBlur(GraphicsDevice graphics)
             : base(graphics)
        {
        }

        public override void Initialize(ContentManager content)
        {
            m_Effect = content.Load<Effect>("Shaders/PostProcessing/SimpleBlur");
            m_SceneRenderTarget = GetRenderTarget();
        }

        protected override void OnVRChanged(VRService service)
        {
            base.OnVRChanged(service);
            m_SceneRenderTarget.Dispose();
            m_SceneRenderTarget = GetRenderTarget();
        }

        public override void Draw(SpriteBatch spriteBatch, RenderTarget2D sceneRT)
        {
            m_GraphicsDevice.SetRenderTarget(m_SceneRenderTarget);
            m_GraphicsDevice.SamplerStates[1] = SamplerState.LinearClamp;

            m_Effect.Parameters["BlurDistance"].SetValue(BlurDistance);

            DrawFullscreenQuad(spriteBatch, sceneRT, m_SceneRenderTarget, m_Effect);

            m_GraphicsDevice.SetRenderTarget(null);
            m_GraphicsDevice.Textures[1] = m_SceneRenderTarget;

            var viewport = m_GraphicsDevice.Viewport;
            m_GraphicsDevice.SetRenderTarget(sceneRT);

            DrawFullscreenQuad(spriteBatch, m_SceneRenderTarget, m_SceneRenderTarget.Width, m_SceneRenderTarget.Height, null);
        }
    }
}
