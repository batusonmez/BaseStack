import { DataTableConfig } from "../DataTable/Models/DataTableConfig";
import { CellTemplates } from "../DataTable/templates/CellTemplates";

export class BaseEditor {
    public AddEditCell(config: DataTableConfig): void {
        if (config.Data && config.Data.length) {
            config.Cells = config.Cells.filter(d => d.Binder != "_edit");
            config.Cells.push({
                Binder: "_edit",
                HeaderName: ""
            });
            
            for (let i = 0; i < config.Data.length; i++) {
                config.Data[i]._edit=CellTemplates.EDIT_BUTTON;
                    
            }
        }
    }
}