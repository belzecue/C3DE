﻿using C3DE.Graphics.Geometries;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Runtime.Serialization;

namespace C3DE.Components.Rendering
{
    [DataContract]
    public class MeshRenderer : Renderer
    {
        private bool _haveListener;
        protected Geometry geometry;

        [DataMember]
        public Geometry Geometry
        {
            get { return geometry; }
            set
            {
                if (value != geometry && value != null)
                {
                    if (geometry != null && _haveListener)
                    {
                        geometry.ConstructionDone -= ComputeBoundingInfos;
                        _haveListener = false;
                    }

                    geometry = value;

                    geometry.ConstructionDone += ComputeBoundingInfos;
                    _haveListener = true;
                }
            }
        }

        public MeshRenderer()
            : base()
        {
        }

        public override void ComputeBoundingInfos()
        {
            if (geometry == null)
                return;

            var min = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
            var max = new Vector3(float.MinValue, float.MinValue, float.MinValue);
            var vertices = geometry.GetVertices(VertexType.Position);

            for (int i = 0, l = vertices.Length; i < l; i++)
            {
                min.X = Math.Min(vertices[i].X, min.X);
                min.Y = Math.Min(vertices[i].Y, min.Y);
                min.Z = Math.Min(vertices[i].Z, min.Z);
                max.X = Math.Max(vertices[i].X, max.X);
                max.Y = Math.Max(vertices[i].Y, max.Y);
                max.Z = Math.Max(vertices[i].Z, max.Z);
            }

            boundingBox.Min = min;
            boundingBox.Max = max;

            var dx = max.X - min.X;
            var dy = max.Y - min.Y;
            var dz = max.Z - min.Z;

            boundingSphere.Radius = (float)Math.Max(Math.Max(dx, dy), dz) / 2.0f;
            boundingSphere.Center = transform.Position;

            UpdateColliders();
        }

        public override void Draw(GraphicsDevice device)
        {
            device.SetVertexBuffer(geometry.VertexBuffer);
            device.Indices = geometry.IndexBuffer;
            device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, geometry.Indices.Length / 3);
        }

        public override void Dispose()
        {
            if (geometry != null)
            {
                geometry.Dispose();
                geometry = null;
            }
        }

        public override void PostDeserialize()
        {
            if (geometry != null && geometry.Built)
                geometry.Build();
        }

        public override object Clone()
        {
            var clone = (MeshRenderer)base.Clone();

            if (geometry != null)
            {
                clone.geometry = (Geometry)Activator.CreateInstance(geometry.GetType());
                clone.geometry.Size = geometry.Size;
                clone.geometry.TextureRepeat = geometry.TextureRepeat;

                if (geometry.Built)
                    clone.geometry.Build();
            }
              
            return clone;
        }
    }
}