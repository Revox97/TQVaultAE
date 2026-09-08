using TQVaultAE.Model.Vaults;
using TQVaultAE.Model;
using TQVaultAE.Model.Enumerations;

namespace TQVaultAE.Application.Contracts
{
    /// <summary>
    /// Represents a service, that allows to interact with TQVault vaults.
    /// </summary>
    public interface IVaultService
    {
        /// <summary>
        /// Gets a list of all <see cref="Vault"/>s of the <see cref="User"/>.
        /// </summary>
        /// <returns>A list of all vaults of a <see cref="User"/>.</returns>
        Task<List<Vault>> GetVaultsAsync();

        /// <summary>
        /// Creates a new <see cref="Vault"/>.
        /// </summary>
        /// <param name="name">The name of the new <see cref="Vault"/>.</param>
        /// <param name="type">The <see cref="VaultType"/> of the new <see cref="Vault"/>.</param>
        /// <returns>The newly created <see cref="Vault"/>.</returns>
        Task<Vault> CreateVaultAsync(string name, VaultType type);

        /// <summary>
        /// Updates a <see cref="Vault"/> in persistent storage.
        /// </summary>
        /// <param name="vault">The <see cref="Vault"/>, that should be updated.</param>
        Task UpdateVaultAsync(Vault vault);

        /// <summary>
        /// Deletes a <see cref="Vault"/>.
        /// </summary>
        /// <param name="vault">The <see cref="Vault"/>, that should be deleted.</param>
        Task DeleteVaultAsync(Vault vault);
    }
}
