#version 330

in  vec2 texCoords;
in  vec3 vertNormal;
in  vec3 noiseVec;
in float noise;
in float oTime;

out vec4 fragColor;

uniform sampler2D myTexture0;

void main() { 

	vec4 black = vec4(0.0f, 0.0f, 0.0f, 1.0f);
	vec4 white = vec4(1.0f, 1.0f, 1.0f, 1.0f);
	vec4 red = vec4(1.0f, 0.0f, 0.0f, 1.0f);
	vec4 yellow = vec4(1.0f, 1.0f, 0.0f, 1.0f);
	vec4 mixture = mix(black, red, length(noiseVec));

	if(length(noiseVec) > 1.0f) {
		mixture = mix(red, yellow, length(noiseVec) - 1.0f);
	}
	if(length(noiseVec) > 2.0f) {
		mixture = mix(yellow, white, length(noiseVec) - 2.0f);
	}

	fragColor = mixture;
	//fragColor = texture2D( myTexture0, texCoords + oTime);
}

/*	Funny phong texture stuff dont mind this

	/// I could have passed these in as Uniforms but for simplicity, 
	/// I'll just define them here: specular, diffuse, ambient for the surface material 
	const vec4 ks = vec4(0.5, 0.2, 0.2, 0.0);
	const vec4 tint = vec4(0.2, 0.0, 0.0, 0.0);
	
	/// The reflect() method this most GPU expensive step in this proceedure
	/// The Blinn-Phong method is faster.   	
	vec3 reflection = normalize(reflect(-lightDir, vertNormal));
	float diff = max(dot(vertNormal, lightDir), 0.0);
	float spec = max(0.0, dot(vertNormal, reflection));
	if(diff != 0.0){
		spec = pow(spec,16.0);
	}
	vec4 kd = texture2D(myTexture0, texCoords);
	vec4 ka = 0.1 * kd;
	fragColor =  ka + (diff  * kd) + (spec * ks);*/