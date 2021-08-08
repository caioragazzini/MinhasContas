Desafio - Loja Virtual
Tecnologias Utilizadas
•	Visual Studio 2015 Professional 
•	WebApplication
•	WebApi
•	MVC4
•	C#
•	Banco de Dados SqlServer
Inicio
Acesso: http://localhost:55355/ProdutosController

 
1-Classe Produtos
2-Classe ProdutosModel
3-Controle ProdutosController
5-View
6-Banco de Dados
1-	Criar a classe Produtos:



public class clsProdutos
    {
        public int IdProduto { get; set; }
        public string NomeProduto { get; set; }
        public decimal ValorProduto { get; set; }
        public int QtdeProduto { get; set; }
      

    }

2-	Criar a Classe ProdutosModel onde será realizada a lógica de acesso a dados, nesta classe foi herdada a Interface IDisposable que será responsável pelo fechamento da conexão, que se encontra no arquivo Web.config.
Métodos da Classe ProdutosModel:
•	Método Create(): responsável por inserir registros na tabela tbProdutos.
•	Método CreateProc(): responsável por inserir através de uma procedure registros da tabela tbProdutos.
•	Método Updade(): responsável por atualizar dados na tabela tbProdutos.
•	Método Delete(): responsável por apagar registros na tabela tbProdutos.
•	Método Read(): vai retornar uma lista com os registros da tabela tbProdutos.
•	Método ReadId (): vai retornar uma lista com registros da tabela produtos por IdProduto.




public class ProdutoModel:IDisposable
    {

        private SqlConnection connection;

        public ProdutoModel()
        {
            string strConn = WebConfigurationManager.ConnectionStrings["DbLojaVirtual"].ConnectionString;

            connection = new SqlConnection(strConn);
            connection.Open();

        }

        public void Dispose()
        {
            connection.Close();
        }

        public void Create(clsProdutos produto)
        {

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"INSERT INTO tbProdutos(NomeProduto,ValorProduto,QtdeProduto) VALUES (@NomeProduto,@ValorProduto,@QtdeProduto)";

            cmd.Parameters.AddWithValue("@NomeProduto", produto.NomeProduto);
            cmd.Parameters.AddWithValue("@ValorProduto", produto.ValorProduto);
            cmd.Parameters.AddWithValue("@QtdeProduto", produto.QtdeProduto);
      
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SqlException error)
            {


            }
        }

        public void CreateProc(clsProdutos produto)
        {

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = ("sp_InsertProdutos");
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@NomeProduto", produto.NomeProduto);
            cmd.Parameters.AddWithValue("@ValorProduto", produto.ValorProduto);
            cmd.Parameters.AddWithValue("@QtdeProduto", produto.QtdeProduto);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SqlException error)
            {


            }


        }



        public void Update(clsProdutos produto)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"UPDATE tbProdutos SET NomeProduto=@NomeProduto,ValorProduto=@ValorProduto,QtdeProduto=@QtdeProduto WHERE IdProduto=@IdProduto";

            cmd.Parameters.AddWithValue("@NomeProduto", produto.NomeProduto);
            cmd.Parameters.AddWithValue("@ValorProduto", produto.ValorProduto);
            cmd.Parameters.AddWithValue("@QtdeProduto", produto.QtdeProduto);
            cmd.Parameters.AddWithValue("@IdProduto", produto.IdProduto);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SqlException error)
            {


            }
        }

        public void Delete(clsProdutos produto)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"DELETE FROM tbProdutos WHERE IdProduto=@IdProduto";

            cmd.Parameters.AddWithValue("@IdProduto", produto.IdProduto);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SqlException error)
            {


            }
        }


        public List<clsProdutos> Read()
        {
            List<clsProdutos> lista = new List<clsProdutos>();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"SELECT * FROM tbProdutos";

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {

                clsProdutos produto = new clsProdutos();
                produto.IdProduto = (int)reader[0];
                produto.NomeProduto = (string)reader[1];
                produto.ValorProduto = (decimal)reader[2];
                produto.QtdeProduto = (int)reader[3];
                

                lista.Add(produto);
            }

            return lista;

        }

        public SqlDataReader GetProdutos()
        {
            

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"SELECT NomeProduto,ValorProduto FROM tbProdutos";

            SqlDataReader reader = cmd.ExecuteReader();

            return reader;
        }

        public clsProdutos ReadId(int id)
        {

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"SELECT * FROM tbProdutos where IdProduto=@IdProduto";

            SqlParameter param;
            param = cmd.Parameters.AddWithValue("@IdProduto", id);
            SqlDataReader reader = cmd.ExecuteReader();
            clsProdutos produto = null;
            try
            {

                while (reader.Read())
                {
                    produto = new clsProdutos();
                    produto.IdProduto = (int)reader[0];
                    produto.NomeProduto = (string)reader[1];
                    produto.ValorProduto = (decimal)reader[2];
                    produto.QtdeProduto = (int)reader[3];
                }

            }
            catch (Exception e)
            {
                throw e;
            }
            return produto;

        }

    }
}



3-	Criar o Controle(ProdutosController) responsável por fazer a ligação com a classe ProdutoModel:
•	ActionResult Index():chama o método Read() da classe ProdutoModel onde sera retornado uma lista de produtos que abastecerá a View Index.
•	ActionResult Create: responsável por chamar o método Create() que armazenar os dados de um produto.
•	ActionResult CreateProc: responsável por chamar o método CreateProc() que armazena os dados de um produto através de uma Procedure.
•	ActionResult Edit(): recebe um idProdutos da View Index e chama o método Edit() quie atualiza os dados de um produto.
•	ActionResult Delete(): recebe um idProdutos da View Index e chama o método Delete() que exclui um registro da tabela produtos.
•	ActionResult Details(): recebe um idProdutos da View Index e chama o método ReadId() que exibirá um registro da tabela produtos.



public class ProdutosController : Controller
    {
        // GET:Produtos
        public ActionResult Index()
        {
            using (ProdutoModel model = new ProdutoModel())
            {
                List<clsProdutos> lista = model.Read();
                return View(lista);
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            clsProdutos produto = new clsProdutos();
            produto.NomeProduto = form["NomeProduto"];
            produto.ValorProduto = Convert.ToDecimal(form["ValorProduto"]);
            produto.QtdeProduto = Convert.ToInt32(form["QtdeProduto"]);
           

            using (ProdutoModel model = new ProdutoModel())
            {
                model.Create(produto);
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult CreateProc()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateProc(FormCollection form)
        {
            clsProdutos produto = new clsProdutos();
            produto.NomeProduto = form["NomeProduto"];
            produto.ValorProduto = Convert.ToDecimal(form["ValorProduto"]);
            produto.QtdeProduto = Convert.ToInt32(form["QtdeProduto"]);
            using (ProdutoModel model = new ProdutoModel())
            {
                model.CreateProc(produto);
                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(int id)
        {
            using (ProdutoModel model = new ProdutoModel())
            {
                var produto = model.ReadId(id);

                if (produto == null)
                {
                    return HttpNotFound();
                }

                return View(produto);
            }

        }

        [HttpPost]
        public ActionResult Edit(clsProdutos produto)
        {
            using (ProdutoModel model = new ProdutoModel())
            {
                if (ModelState.IsValid)
                {
                    model.Update(produto);
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(produto);
                }
            }
        }


        //POST: Produtos/Delete/5
        public ActionResult Delete(int id)
        {
            using (ProdutoModel model = new ProdutoModel())
            {
                var produto = model.ReadId(id);

                if (produto == null)
                {
                    return HttpNotFound();
                }

                return View(produto);
            }
        }

        [HttpPost]
        public ActionResult Delete(clsProdutos produto)
        {
            using (ProdutoModel model = new ProdutoModel())
            {
                if (ModelState.IsValid)
                {
                    model.Delete(produto);
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(produto);
                }
            }
        }

        public ActionResult Details(int id)
        {
            using (ProdutoModel model = new ProdutoModel())
            {
                var produto = model.ReadId(id);

                if (produto == null)
                {
                    return HttpNotFound();
                }

                return View(produto);
            }

        }
       
    }
}

4-	Views foi criada uma View para cada ActionResult:
•	Create.cshtml
•	CreateProc.cshtml
•	Delete.cshtml
•	Details.cshtml
•	Edit.cshtml
•	Index.cshtml

4.1- Create.cshtml:
@model LojaVirtual_Produtos.Models.clsProdutos

@using (Html.BeginForm()) 
{
    @Html.AntiForgeryToken()
    
    <div class="form-horizontal">
        <h4>Adicionar Produtos</h4>
        <hr />
        @Html.ValidationSummary(true, "", new { @class = "text-danger" })
      

        <div class="form-group">
            @Html.LabelFor(model => model.NomeProduto, htmlAttributes: new { @class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(model => model.NomeProduto, new { htmlAttributes = new { @class = "form-control" } })
                @Html.ValidationMessageFor(model => model.NomeProduto, "", new { @class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(model => model.ValorProduto, htmlAttributes: new { @class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(model => model.ValorProduto, new { htmlAttributes = new { @class = "form-control" } })
                @Html.ValidationMessageFor(model => model.ValorProduto, "", new { @class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(model => model.QtdeProduto, htmlAttributes: new { @class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(model => model.QtdeProduto, new { htmlAttributes = new { @class = "form-control" } })
                @Html.ValidationMessageFor(model => model.QtdeProduto, "", new { @class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <input type="submit" value="Adicionar" class="btn btn-default" />
            </div>
        </div>
    </div>
}

<div>
    @Html.ActionLink("Retornar para lista", "Index")
</div>
<script src="~/Scripts/jquery-1.10.2.min.js"></script>
<script src="~/Scripts/jquery.validate.min.js"></script>
<script src="~/Scripts/jquery.validate.unobtrusive.min.js"></script>
4.2- CreateProc
@model LojaVirtual_Produtos.Models.clsProdutos

@using (Html.BeginForm()) 
{
    @Html.AntiForgeryToken()
    
    <div class="form-horizontal">
        <h4>Adicionar Produtos</h4>
        <hr />
        @Html.ValidationSummary(true, "", new { @class = "text-danger" })
       

        <div class="form-group">
            @Html.LabelFor(model => model.NomeProduto, htmlAttributes: new { @class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(model => model.NomeProduto, new { htmlAttributes = new { @class = "form-control" } })
                @Html.ValidationMessageFor(model => model.NomeProduto, "", new { @class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(model => model.ValorProduto, htmlAttributes: new { @class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(model => model.ValorProduto, new { htmlAttributes = new { @class = "form-control" } })
                @Html.ValidationMessageFor(model => model.ValorProduto, "", new { @class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(model => model.QtdeProduto, htmlAttributes: new { @class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(model => model.QtdeProduto, new { htmlAttributes = new { @class = "form-control" } })
                @Html.ValidationMessageFor(model => model.QtdeProduto, "", new { @class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <input type="submit" value="Adicionar" class="btn btn-default" />
            </div>
        </div>
    </div>
}

<div>
    @Html.ActionLink("Retornar para lista", "Index")
</div>
<script src="~/Scripts/jquery-1.10.2.min.js"></script>
<script src="~/Scripts/jquery.validate.min.js"></script>
<script src="~/Scripts/jquery.validate.unobtrusive.min.js"></script>

4.3-	Delete

@model LojaVirtual_Produtos.Models.clsProdutos


@{
    ViewBag.Title = "Delete";
}
<h3>Voce deseja excluir este produto?</h3>

@using (Html.BeginForm())
{
    @Html.AntiForgeryToken()

    <div class="form-horizontal">
        <h4>Excluir Produtos</h4>
        <hr />
        @Html.ValidationSummary(true, "", new { @class = "text-danger" })
        @Html.HiddenFor(model => model.IdProduto)


        <div class="form-group">
            @Html.LabelFor(model => model.IdProduto, htmlAttributes: new { @class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(model => model.IdProduto, new { htmlAttributes = new { @class = "form-control" } })
                @Html.ValidationMessageFor(model => model.IdProduto, "", new { @class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(model => model.NomeProduto, htmlAttributes: new { @class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(model => model.NomeProduto, new { htmlAttributes = new { @class = "form-control" } })
                @Html.ValidationMessageFor(model => model.NomeProduto, "", new { @class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(model => model.ValorProduto, htmlAttributes: new { @class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(model => model.ValorProduto, new { htmlAttributes = new { @class = "form-control" } })
                @Html.ValidationMessageFor(model => model.ValorProduto, "", new { @class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(model => model.QtdeProduto, htmlAttributes: new { @class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(model => model.QtdeProduto, new { htmlAttributes = new { @class = "form-control" } })
                @Html.ValidationMessageFor(model => model.QtdeProduto, "", new { @class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <input type="submit" value="Excluir" class="btn btn-default" />
            </div>
        </div>
    </div>
}

<div>
    @Html.ActionLink("Retornar para lista", "Index")
</div>

<script src="~/Scripts/jquery-1.10.2.min.js"></script>
<script src="~/Scripts/jquery.validate.min.js"></script>
<script src="~/Scripts/jquery.validate.unobtrusive.min.js"></script>

4.4-	Details

@model LojaVirtual_Produtos.Models.clsProdutos
@{
    ViewBag.Title = "Details";
}
<div>
    <h4>Detalhes do Produto</h4>
    <hr />
    <dl class="dl-horizontal">
        <dt>
            @Html.DisplayNameFor(model => model.IdProduto)
        </dt>

        <dd>
            @Html.DisplayFor(model => model.IdProduto)
        </dd>

        <dt>
            @Html.DisplayNameFor(model => model.NomeProduto)
        </dt>

        <dd>
            @Html.DisplayFor(model => model.NomeProduto)
        </dd>

        <dt>
            @Html.DisplayNameFor(model => model.ValorProduto)
        </dt>

        <dd>
            @Html.DisplayFor(model => model.ValorProduto)
        </dd>

        <dt>
            @Html.DisplayNameFor(model => model.QtdeProduto)
        </dt>

        <dd>
            @Html.DisplayFor(model => model.QtdeProduto)
        </dd>

    </dl>
</div>
<p>
    @Html.ActionLink("Editar", "Edit", new { id = Model.IdProduto }) |
    @Html.ActionLink("Retornar para lista", "Index")
</p>


4.5-	Edit

@model LojaVirtual_Produtos.Models.clsProdutos
@{
    ViewBag.Title = "Edit";
}


@using (Html.BeginForm())
{
    @Html.AntiForgeryToken()

    <div class="form-horizontal">
        <h4>Editar Produto</h4>
        <hr />
        @Html.ValidationSummary(true, "", new { @class = "text-danger" })
        @Html.HiddenFor(model => model.IdProduto)

                
        <div class="form-group">
            @Html.LabelFor(model => model.NomeProduto, htmlAttributes: new { @class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(model => model.NomeProduto, new { htmlAttributes = new { @class = "form-control" } })
                @Html.ValidationMessageFor(model => model.NomeProduto, "", new { @class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(model => model.ValorProduto, htmlAttributes: new { @class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(model => model.ValorProduto, new { htmlAttributes = new { @class = "form-control" } })
                @Html.ValidationMessageFor(model => model.ValorProduto, "", new { @class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(model => model.QtdeProduto, htmlAttributes: new { @class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(model => model.QtdeProduto, new { htmlAttributes = new { @class = "form-control" } })
                @Html.ValidationMessageFor(model => model.QtdeProduto, "", new { @class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <input type="submit" value="Editar" class="btn btn-default" />
            </div>
        </div>
    </div>
}

<div>
    @Html.ActionLink("Retornar para lista", "Index")
</div>

<script src="~/Scripts/jquery-1.10.2.min.js"></script>
<script src="~/Scripts/jquery.validate.min.js"></script>
<script src="~/Scripts/jquery.validate.unobtrusive.min.js"></script>

4.6-	Index

@model IEnumerable<LojaVirtual_Produtos.Models.clsProdutos>

<h1>Loja Virtual Produtos</h1>

@{
    ViewBag.Title = "Index";
}
<p>
    @Html.ActionLink("Criar novo produto", "Create")
</p>
<p>
    @Html.ActionLink("Criar novo produto utilizando procedures", "CreateProc")
</p>


<table class="table"id="tbProdutos">    
    <thead>
        <tr>
            <th>
                @Html.DisplayNameFor(model => model.IdProduto)
            </th>
            <th>
                @Html.DisplayNameFor(model => model.NomeProduto)
            </th>
            <th>
                @Html.DisplayNameFor(model => model.ValorProduto)
            </th>
            <th>
                @Html.DisplayNameFor(model => model.QtdeProduto)
            </th>
            <th></th>
        </tr>
    </thead>
    <tbody>
        @foreach (var item in Model)
        {
            <tr>
                <td>
                    @Html.DisplayFor(modelItem => item.IdProduto)
                </td>
                <td>
                    @Html.DisplayFor(modelItem => item.NomeProduto)
                </td>
                <td>
                    @Html.DisplayFor(modelItem => item.ValorProduto)
                </td>
                <td>
                    @Html.DisplayFor(modelItem => item.QtdeProduto)
                </td>
                <td>
                    @Html.ActionLink("Edit", "Edit", new { id = item.IdProduto }) |
                    @Html.ActionLink("Details", "Details", new { id = item.IdProduto }) |
                    @Html.ActionLink("Delete", "Delete", new { id = item.IdProduto })
                </td>
            </tr>
        }

        </tbody>

    </table>


5-	Tabela Produtos do banco de dados:

CREATE TABLE [dbo].[tbProdutos] (
    [IdProduto]    INT           IDENTITY (1, 1) NOT NULL,
    [NomeProduto]  VARCHAR (150) NULL,
    [ValorProduto] DECIMAL (18)  NULL,
    [QtdeProduto]  INT           NULL,
    PRIMARY KEY CLUSTERED ([IdProduto] ASC)
);
