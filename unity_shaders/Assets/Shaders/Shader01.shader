// See https://www.shadertoy.com/view/4dl3zn
// GLSL -> HLSL reference: https://msdn.microsoft.com/en-GB/library/windows/apps/dn166865.aspx

Shader "Custom/Shader01" {

	SubShader{
		Pass{
			GLSLPROGRAM

			#ifdef VERTEX

			void main(){

				gl_Position = gl_ModelViewProjectionMatrix * gl_Vertex;

			}

			#endif

			#ifdef FRAGMENT

			void main(){

				gl_FragColor = vec4(1.0, 0.0, 0.0, 1.0); 

			}

			#endif

			ENDGLSL
		}
	}
   
}