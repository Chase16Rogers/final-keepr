import { api } from './AxiosService'

class VaultKeepsService {
  async createVaultKeep(vaultId, keepId) {
    const newVK = {}
    newVK.vaultId = vaultId
    newVK.keepId = keepId
    await api.post('api/vaultkeeps', newVK)
  }
}
export const vaultKeepsService = new VaultKeepsService()
