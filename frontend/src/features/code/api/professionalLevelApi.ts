import { BaseApi } from "@/shared/api/baseApi";
import type { ProfessionalLevel } from "@/features/code/model/ProfessionalLevel";

class ProfessionalLevelApi extends BaseApi {
    private readonly baseUrl = "/professional-levels";

    getAll(): Promise<ProfessionalLevel[]> {
        return this.get<ProfessionalLevel[]>(this.baseUrl);
    }
}

export const professionalLevelApi = new ProfessionalLevelApi();
