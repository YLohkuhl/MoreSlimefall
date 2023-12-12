using Il2CppMonomiPark.SlimeRancher;
using Il2CppMonomiPark.SlimeRancher.Weather;
using MelonLoader;
using MoreSlimefall.Data.Weather;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.VFX;
using static MoreSlimefall.Data.Weather.SlimeRainOutbreak;

namespace MoreSlimefall.Components
{
    [RegisterTypeInIl2Cpp]
    internal class SlimeRainOutbreakVFXControl : MonoBehaviour
    {
        private SlimeRainVFXControl slimeRainControl;
        private VisualEffect slimeRainVisualFX;

        private Color defaultCloudSlimeColor;
        private Color defaultCloudTintColor;
        private Color defaultVisualFXColor;

        private Color currentCloudSlimeColor;
        private Color currentCloudTintColor;
        private Color currentVisualFXColor;

        private bool isTransitioning;
        private float intensity;

        /*private bool isTransitioning;
        private bool isOutbreak;*/

        void Start()
        {
            slimeRainControl = GetComponent<SlimeRainVFXControl>();
            slimeRainVisualFX = slimeRainControl?.gameObject?.GetComponentInChildren<VisualEffect>();
            if (!slimeRainControl || !slimeRainVisualFX)
                Destroy(this);

            defaultCloudSlimeColor = slimeRainControl._slimeRainCloud.material.GetColor("_SlimeColor");
            defaultCloudTintColor = slimeRainControl._slimeRainCloud.material.GetColor("_Tint");
            defaultVisualFXColor = slimeRainVisualFX.GetVector4("Color");

            currentCloudSlimeColor = defaultCloudSlimeColor;
            currentCloudTintColor = defaultCloudTintColor;
            currentVisualFXColor = defaultVisualFXColor;
        }

        void Update() => TryProcessTransition(intensity);

        public void SetSlimeRainOutbreak(bool value)
        {
            if (value)
            {
                intensity = 1;
                isTransitioning = true;
            }
            else if (!value)
            {
                intensity = 0;
                isTransitioning = true;
            }

            /*if (value)
            {
                slimeRainVisualFX.SetVector4("Color", Color.black);
                slimeRainControl._slimeRainCloud.material.SetColor("_Tint", Color.black);
                slimeRainControl._slimeRainCloud.material.SetColor("_SlimeColor", outbreakColor);
            }
            else if (!value)
            {
                slimeRainVisualFX.SetVector4("Color", defaultVisualFXColor);
                slimeRainControl._slimeRainCloud.material.SetColor("_Tint", defaultVisualFXColor);
                slimeRainControl._slimeRainCloud.material.SetColor("_SlimeColor", defaultCloudSlimeColor);
            }*/

            /*if (value)
            {
                isOutbreak = true;
                isTransitioning = true;
            }
            else if (!value)
            {
                isOutbreak = false;
                isTransitioning = true;
            }*/
        }

        void TryProcessTransition(float intensity, float transitionSpeed = 2)
        {
            if (isTransitioning && intensity > 0)
            {
                var slimeRainCloud = slimeRainControl._slimeRainCloud.material;

                currentVisualFXColor = slimeRainVisualFX.GetVector4("Color");
                slimeRainVisualFX.SetVector4("Color", Vector4.Lerp(currentVisualFXColor, Color.black, Time.deltaTime * transitionSpeed));

                currentCloudSlimeColor = slimeRainCloud.GetColor("_SlimeColor");
                slimeRainCloud.SetColor("_SlimeColor", Color.Lerp(currentCloudSlimeColor, outbreakColor, Time.deltaTime * transitionSpeed));

                currentCloudTintColor = slimeRainCloud.GetColor("_Tint");
                slimeRainCloud.SetColor("_Tint", Color.Lerp(currentCloudTintColor, Color.black, Time.deltaTime * transitionSpeed));

                if (slimeRainVisualFX.GetVector4("Color").Equals(Color.black)
                        && slimeRainCloud.GetColor("_SlimeColor").Equals(outbreakColor)
                            && slimeRainCloud.GetColor("_Tint").Equals(Color.black))
                    isTransitioning = false;
            }
            else if (isTransitioning && intensity <= 0)
            {
                var slimeRainCloud = slimeRainControl._slimeRainCloud.material;

                currentVisualFXColor = slimeRainVisualFX.GetVector4("Color");
                slimeRainVisualFX.SetVector4("Color", Vector4.Lerp(currentVisualFXColor, defaultVisualFXColor, Time.deltaTime * transitionSpeed));

                currentCloudSlimeColor = slimeRainCloud.GetColor("_SlimeColor");
                slimeRainCloud.SetColor("_SlimeColor", Color.Lerp(currentCloudSlimeColor, defaultCloudSlimeColor, Time.deltaTime * transitionSpeed));

                currentCloudTintColor = slimeRainCloud.GetColor("_Tint");
                slimeRainCloud.SetColor("_Tint", Color.Lerp(currentCloudTintColor, defaultCloudTintColor, Time.deltaTime * transitionSpeed));

                if (slimeRainVisualFX.GetVector4("Color").Equals(defaultVisualFXColor)
                        && slimeRainCloud.GetColor("_SlimeColor").Equals(defaultCloudSlimeColor)
                            && slimeRainCloud.GetColor("_Tint").Equals(defaultCloudTintColor))
                    isTransitioning = false;
            }
        }

        /*public bool IsColor(Material material, string property, Color color) => material.GetColor(property) == color;

        public bool IsVector4(VisualEffect visualEffect, string property, Color color) => visualEffect.GetVector4(property) == new Vector4(color.r, color.g, color.b, color.a);

        internal void TransitionColor(Material material, string property, Color startColor, Color endColor, float speed = 0.01f)
        {
            if (material.GetColor(property) != endColor)
                material.SetColor(property, Color.Lerp(startColor, endColor, speed));
        }

        internal void TransitionVector4(VisualEffect visualEffect, string property, Color startColor, Color endColor, float speed = 0.01f)
        {
            var startVector4 = new Vector4(startColor.r, startColor.g, startColor.b, startColor.a);
            var endVector4 = new Vector4(endColor.r, endColor.g, endColor.b, endColor.a);

            if (visualEffect.GetVector4(property) != endVector4)
                visualEffect.SetVector4(property, Vector4.Lerp(startVector4, endVector4, speed));
        }

        void TryProcessTransition()
        {
            if (isTransitioning && isOutbreak)
            {
                var slimeCloudMaterial = slimeRainControl._slimeRainCloud.material;
                TransitionVector4(slimeRainVisualFX, "Color", defaultVisualFXColor, outbreakColor);
                TransitionColor(slimeCloudMaterial, "_SlimeColor", defaultCloudSlimeColor, outbreakColor);
                TransitionColor(slimeCloudMaterial, "_Tint", defaultCloudTintColor, outbreakTintColor);

                if (IsVector4(slimeRainVisualFX, "Color", outbreakColor) && IsColor(slimeCloudMaterial, "_SlimeColor", outbreakColor) && IsColor(slimeCloudMaterial, "_Tint", outbreakTintColor))
                    isTransitioning = false;
            }
            else if (isTransitioning && !isOutbreak)
            {
                var slimeCloudMaterial = slimeRainControl._slimeRainCloud.material;
                TransitionVector4(slimeRainVisualFX, "Color", outbreakColor, defaultVisualFXColor);
                TransitionColor(slimeCloudMaterial, "_SlimeColor", outbreakColor, defaultCloudSlimeColor);
                TransitionColor(slimeCloudMaterial, "_Tint", outbreakTintColor, defaultCloudTintColor);

                if (IsVector4(slimeRainVisualFX, "Color", defaultVisualFXColor) && IsColor(slimeCloudMaterial, "_SlimeColor", defaultCloudSlimeColor) && IsColor(slimeCloudMaterial, "_Tint", defaultCloudTintColor))
                    isTransitioning = false;
            }
        }*/
    }
}
