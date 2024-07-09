Shader "Custom/OutlineShader"
{
    Properties
    {
        _Color ("Main Color", Color) = (1,1,1,1)
        _OutlineColor ("Outline Color", Color) = (1,0,0,1) // Default outline color is red
        _Outline ("Outline width", Range (.002, 0.03)) = .005
    }
    SubShader
    {
        Tags {"Queue" = "Overlay"}
        LOD 100

        // Pass for the main object rendering
        Pass
        {
            Name "BASE"
            Tags {"LightMode" = "Always"}
            Cull Front
            ZWrite On
            ZTest LEqual

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : POSITION;
                float4 color : COLOR;
            };

            fixed4 _OutlineColor;
            float _Outline;
            float4 _Color;

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.color = _Color;
                return o;
            }

            fixed4 frag(v2f i) : COLOR
            {
                return i.color;
            }
            ENDCG
        }

        // Pass for the outline rendering
        Pass
        {
            Name "OUTLINE"
            Tags {"LightMode" = "Always"}
            Cull Back
            ZWrite On
            ColorMask RGB
            Blend SrcAlpha OneMinusSrcAlpha
            Offset 7, 7

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : POSITION;
                float4 color : COLOR;
            };

            fixed4 _OutlineColor;
            float _Outline;

            v2f vert(appdata v)
            {
                // Scale vertices along normals
                v2f o;
                float3 norm = mul((float3x3) UNITY_MATRIX_IT_MV, v.normal);
                o.pos = UnityObjectToClipPos(v.vertex + float4(norm * _Outline, 0));
                o.color = _OutlineColor;
                return o;
            }

            fixed4 frag(v2f i) : COLOR
            {
                return i.color;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
