shader_type canvas_item;

uniform sampler2D noise;
uniform float speed = 1.0;
uniform vec4 light_color: source_color = vec4(0.2,0.2,0.4, 1);

void fragment() {
	vec2 uv = vec2(UV.x + TIME*speed, UV.y);
	vec4 noise_rgb = texture(noise, uv);
	float new_alpha = noise_rgb.r * noise_rgb.g * noise_rgb.b;
	
	COLOR.rgb = light_color.rgb;
	COLOR.a = clamp(noise_rgb.a * 0.3 * texture(TEXTURE, UV).a, 0.0, 1.0);
	// Place fragment code here.
}
