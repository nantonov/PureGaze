import { DataGrid, type GridColDef, type GridPaginationModel } from "@mui/x-data-grid";

type PaginationTableProps = {
    rows: any[];
    columns: GridColDef<any>[];
    rowCount: number;
    loading: boolean;
    paginationMode: "client" | "server";
    paginationModel: GridPaginationModel;
    onPaginationModelChange: (model: GridPaginationModel) => void;
    pageSizeOptions: number[] | [5, 10, 20, 50];
    disableRowSelectionOnClick: boolean;
}

export function PaginationTable(props : PaginationTableProps){
    return(
        <DataGrid
            rows={props.rows}
            columns={props.columns}
            rowCount={props.rowCount}
            loading={props.loading}
            paginationMode={props.paginationMode}
            paginationModel={props.paginationModel}
            onPaginationModelChange={props.onPaginationModelChange}
            pageSizeOptions={props.pageSizeOptions}
            disableRowSelectionOnClick
        />  
    );
}