import { BaseApi } from "@/shared/api/baseApi";
import type { CreateCodeRequest } from "@/entities/code/CreateCodeRequest.ts";
import type { UpdateCodeRequest } from "@/entities/code/UpdateCodeRequest.ts";
import type { GetCode } from "@/entities/code/GetCode.ts";
import type { Code } from "@/entities/code/Code.ts";

class AdminCodeApi extends BaseApi {
    private readonly baseUrl = "/admin-codes";

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

export const codeApi = new AdminCodeApi();