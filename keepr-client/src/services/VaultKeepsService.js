import { AppState } from '../AppState'
import { api } from './AxiosService'

class VaultKeepsService {
  async createVaultKeep(vaultId, keepId) {
    const newVK = {}
    newVK.vaultId = vaultId
    newVK.keepId = keepId
    await api.post('api/vaultkeeps', newVK)
  }

  async deleteVaultKeep(id) {
    await api.delete('api/vaultkeeps/' + id)
    const index = AppState.keeps.findIndex(k => k.vaultKeepId === id)
    AppState.keeps.splice(index, 1)
  }
}
export const vaultKeepsService = new VaultKeepsService()
