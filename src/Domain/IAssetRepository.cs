namespace Giacomelli.Unity.Metadata.Domain
{
	/// <summary>
	/// Defines an interface for asset repository.
	/// </summary>
    public interface IAssetRepository
	{
		/// <summary>
		/// Gets a game object in the specified path.
		/// </summary>
		/// <returns>The game object.</returns>
		/// <param name="path">The asset path.</param>
        IGameObject GetGameObject(string path);
    }
}
