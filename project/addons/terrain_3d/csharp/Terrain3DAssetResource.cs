using System;
using Godot;

namespace TokisanGames;

/// <summary>
/// Common C# type for the native abstract Terrain3DAssetResource. Native asset
/// resources cannot be instantiated directly; bind their concrete mesh or
/// texture type instead.
/// </summary>
[Tool]
public abstract partial class Terrain3DAssetResource : Resource
{
	protected Terrain3DAssetResource() { }

	/// <summary>
	/// Attaches the matching concrete wrapper to a native Terrain3D asset.
	/// </summary>
	public static Terrain3DAssetResource? Bind(GodotObject? godotObject)
	{
		if (godotObject is null || !IsInstanceValid(godotObject))
			return null;

		return godotObject.GetClass() switch
		{
			"Terrain3DMeshAsset" => Terrain3DMeshAsset.Bind(godotObject),
			"Terrain3DTextureAsset" => Terrain3DTextureAsset.Bind(godotObject),
			var nativeClass => throw new ArgumentException($"Unsupported Terrain3D asset class: {nativeClass}", nameof(godotObject)),
		};
	}

	/// <summary>Loads and binds a native mesh or texture asset.</summary>
	public static Terrain3DAssetResource? Load(string path) => Bind(ResourceLoader.Load(path));

	public class GDExtensionSignalName : Resource.SignalName { }
	public class GDExtensionPropertyName : Resource.PropertyName { }
	public class GDExtensionMethodName : Resource.MethodName { }
}
