shader_type canvas_item;

uniform vec4 light_color: source_color = vec4(1.0);
uniform float color_modifier: hint_range(0.0, 1.0, 0.01);

void fragment() {
	vec4 color = texture(TEXTURE, UV);
	color.rgb = mix(color.rgb, light_color.rgb, color_modifier);
	COLOR = color;
}
