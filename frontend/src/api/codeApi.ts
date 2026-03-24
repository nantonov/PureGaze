import { BaseApi } from "@/api/baseApi.ts";
import type { Code } from "@/types/Code/Code";
import type { CreateCodeRequest } from "@/types/Code/CreateCodeRequest";
import type { UpdateCodeRequest } from "@/types/Code/UpdateCodeRequest";

class CodeApi extends BaseApi {
    private readonly baseUrl = "/codes";

    getAll(): Promise<Code[]> {
        return this.get<Code[]>(this.baseUrl);
    }

    create(req: CreateCodeRequest): Promise<void> {
        return this.post<void>(this.baseUrl, req);
    }

    update(req: UpdateCodeRequest): Promise<void> {
        return this.put<void>(this.baseUrl, req);
    }

    deleteCode(id: number): Promise<void> {
        return this.delete<void>(`${this.baseUrl}/${id}`);
    }
}

export const codeApi = new CodeApi();