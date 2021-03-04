import { AppState } from '../AppState'
import { api } from './AxiosService'

class VaultsService {
  async getVaultsByProfile(id) {
    const res = await api.get('api/profiles/' + id + '/vaults')
    const vaultArr = res.data
    const resArr = res.data
    vaultArr.forEach(v => {
      if (v.isPrivate && AppState.profile.id !== AppState.account.id) {
        const index = resArr.findIndex(r => r.id === v.id)
        resArr.splice(index, 1)
      }
    })
    AppState.vaults = resArr
  }

  async getUsersVaults() {
    const id = AppState.account.id
    const res = await api.get('api/profiles/' + id + '/vaults')
    AppState.usersVaults = res.data
  }

  async getVaultById(id) {
    const res = await api.get('api/vaults/' + id)
    AppState.activeVault = res.data
  }

  async createVault(data) {
    const res = await api.post('api/vaults', data)
    AppState.vaults.push(res.data)
  }
}
export const vaultsService = new VaultsService()
