import { BaseApi } from "@/api/baseApi.ts";
import type { CreateCodeRequest } from "@/types/Code/CreateCodeRequest";
import type { UpdateCodeRequest } from "@/types/Code/UpdateCodeRequest";
import type { GetCode } from "@/types/Code/GetCode.ts";
import type { Code } from "@/types/Code/Code.ts";

class CodeApi extends BaseApi {
    private readonly baseUrl = "/codes";

    getAll(): Promise<GetCode[]> {
        return this.get<GetCode[]>(this.baseUrl);
    }

    getById(id: number): Promise<Code> {
        return this.get<Code>(`${this.baseUrl}/${id}`);
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