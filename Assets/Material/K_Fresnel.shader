Shader "Unlit/K_Fresnel"
{
    Properties
    {
        _FresnelIntensity("Fresnel Intensity", Range(0, 20)) = 0.3
        _FresnelColor("Fresnel Color", Color) = (1,0,0,1)
        _Albedo("Albedo", Color) = (0,0,0,0)
    }

        SubShader
    {
        Tags {"RenderType" = "Opaque"}

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            float4 _FresnelColor, _Albedo;
            float _FresnelIntensity;

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION; // Le SV_POSITION va gerer le clip space (espace camera), pas besoin de preciser dans le frag shader apres

                float3 ws_Position : TEXCOORD1; //conversion des vertexs en f loat3

                float3 ws_Normal : TEXCOORD0;
            };

            v2f vert(appdata v)
            {
                v2f o;

                o.vertex = UnityObjectToClipPos(v.vertex); //transform nos vertexs en espace camera

                o.ws_Position = mul(unity_ObjectToWorld, v.vertex).xyz; 

                o.ws_Normal = normalize(mul(unity_ObjectToWorld, float4(v.normal, 0))).xyz;

                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                float3 N = normalize(i.ws_Normal); //vecteur de la normal du vertex.
                float3 V = normalize(_WorldSpaceCameraPos - i.ws_Position); //vecteur normalize qui va du vertex vers la camera.

                float NdotV = 1.0 - dot(N,V); // petit dot product pour voir a quel point les deux vecteurs vont dans le meme sens (echelle 0 à 1).

                float4 fresnel = pow(NdotV, _FresnelIntensity) * _FresnelColor; // on boost notre dot product et on le multiplie par la couleur.
                
                return _Albedo + fresnel; // on additionne les deux couleurs.
            }
            ENDCG
        }
    }
}