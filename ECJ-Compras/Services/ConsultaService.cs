using Dominio.Context;
using Dominio.Models;
using ECJ_Compras.Dto;
using ECJ_Compras.Enums;
using ECJ_Compras.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace ECJ_Compras.Services
{
    public class ConsultaService : IConsultaService
    {
        private readonly EjcContext _context;

        public ConsultaService(EjcContext context)
        {
            _context = context;
        }

        public List<Doacao> BuscarDoacoes(PesquisaDoacoesDto doacaoDto)
        {
            var doacoes = _context.Doacoes.Include(d=>d.Equipe).Include(d=>d.Produto).AsQueryable();
            if (!string.IsNullOrEmpty(doacaoDto.CategoriaProduto))
            {
                doacoes = doacoes.Where(d => d.Produto.Categoria.Contains(doacaoDto.CategoriaProduto));
            }
            if (doacaoDto.DataInicio.HasValue && doacaoDto.DataFim.HasValue)
            {
                doacoes = doacoes.Where(d => d.Data >= doacaoDto.DataInicio && d.Data<= doacaoDto.DataFim.Value.AddDays(1));
            }
            if (!string.IsNullOrEmpty(doacaoDto.Equipe))
            {
                doacoes = doacoes.Where(d => d.Equipe.Nome.Contains(doacaoDto.Equipe));
            }

            if(!string.IsNullOrEmpty(doacaoDto.Ordenar))
            {
                if(doacaoDto.OrdenarDecrescente)
                {
                    switch (doacaoDto.Ordenar)
                    {
                        case "Data":
                            doacoes = doacoes.OrderByDescending(d => d.Data);
                            break;
                        case "Equipe":
                            doacoes = doacoes.OrderByDescending(d => d.Equipe);
                            break;
                        case "Produto":
                            doacoes = doacoes.OrderByDescending(d => d.Produto);
                            break;
                        case "Quantidade":
                            doacoes = doacoes.OrderByDescending(d => d.Quantidade);
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    switch (doacaoDto.Ordenar)
                    {
                        case "Data":
                            doacoes = doacoes.OrderBy(d => d.Data);
                            break;
                        case "Equipe":
                            doacoes = doacoes.OrderBy(d => d.Equipe);
                            break;
                        case "Produto":
                            doacoes = doacoes.OrderBy(d => d.Produto);
                            break;
                        case "Quantidade":
                            doacoes = doacoes.OrderBy(d => d.Quantidade);
                            break;
                        default:
                            break;
                    }
                }
            }

            List<Doacao> result = doacoes.ToList();
            return result;
        }

        public List<Transacao> BuscarLancamentos(PesquisaLancamentosDto lancamentosDto)
        {
            var transacoes = _context.Transacoes.Include(d => d.Usuario).AsQueryable();
            if (!string.IsNullOrEmpty(lancamentosDto.Descricao))
            {
                transacoes = transacoes.Where(d => d.Descricao.Contains(lancamentosDto.Descricao));
            }
            if (lancamentosDto.DataInicio.HasValue && lancamentosDto.DataFim.HasValue)
            {
                transacoes = transacoes.Where(d => d.Data >= lancamentosDto.DataInicio && d.Data <= lancamentosDto.DataFim.Value.AddDays(1));
            }
            if (!string.IsNullOrEmpty(lancamentosDto.Tipo))
            {
                transacoes = transacoes.Where(d => d.Tipo.Contains(lancamentosDto.Tipo));
            }
            if (!string.IsNullOrEmpty(lancamentosDto.MetodoPagamento))
            {
                transacoes = transacoes.Where(d => d.MetodoPagamento.Contains(lancamentosDto.MetodoPagamento));
            }
            if (!string.IsNullOrEmpty(lancamentosDto.Autor))
            {
                transacoes = transacoes.Where(d => d.Usuario != null && d.Usuario.Nome.Contains(lancamentosDto.Autor));
            }


            if (!string.IsNullOrEmpty(lancamentosDto.Ordenar))
            {
                if (lancamentosDto.OrdenarDecrescente)
                {
                    switch (lancamentosDto.Ordenar)
                    {
                        case "Descrição":
                            transacoes = transacoes.OrderByDescending(d => d.Descricao);
                            break;
                        case "Tipo":
                            transacoes = transacoes.OrderByDescending(d => d.Tipo);
                            break;
                        case "Método de Pagamento":
                            transacoes = transacoes.OrderByDescending(d => d.MetodoPagamento);
                            break;
                        case "Data":
                            transacoes = transacoes.OrderByDescending(d => d.Data);
                            break;
                        case "Valor":
                            transacoes = transacoes.OrderByDescending(d => d.Valor);
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    switch (lancamentosDto.Ordenar)
                    {
                        case "Descrição":
                            transacoes = transacoes.OrderBy(d => d.Descricao);
                            break;
                        case "Tipo":
                            transacoes = transacoes.OrderBy(d => d.Tipo);
                            break;
                        case "Método de Pagamento":
                            transacoes = transacoes.OrderBy(d => d.MetodoPagamento);
                            break;
                        case "Data":
                            transacoes = transacoes.OrderBy(d => d.Data);
                            break;
                        case "Valor":
                            transacoes = transacoes.OrderBy(d => d.Valor);
                            break;
                        default:
                            break;
                    }
                }
            }

            List<Transacao> result = transacoes.ToList();
            return result;
        }
        
        public List<Produto> BuscarProdutos(PesquisaProdutosDto produtosDto)
        {
            var produtos = _context.Produtos.AsQueryable();
            if (!string.IsNullOrEmpty(produtosDto.Categoria))
            {
                produtos = produtos.Where(d => d.Categoria.Contains(produtosDto.Categoria));
            }
            if (!string.IsNullOrEmpty(produtosDto.Unidade))
            {
                produtos = produtos.Where(d => d.Unidade != null && d.Unidade.Contains(produtosDto.Unidade));
            }
            if (!string.IsNullOrEmpty(produtosDto.Quantidade))
            {
                produtos = produtos.Where(d => d.Quantidade.Equals(produtosDto.Quantidade));
            }


            if (!string.IsNullOrEmpty(produtosDto.Ordenar))
            {
                if (produtosDto.OrdenarDecrescente)
                {
                    switch (produtosDto.Ordenar)
                    {
                        case "Produto":
                            produtos = produtos.OrderByDescending(d => d.Categoria);
                            break;
                        case "Unidade":
                            produtos = produtos.OrderByDescending(d => d.Unidade);
                            break;
                        case "Quantidade":
                            produtos = produtos.OrderByDescending(d => d.Quantidade);
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    switch (produtosDto.Ordenar)
                    {
                        case "Produto":
                            produtos = produtos.OrderBy(d => d.Categoria);
                            break;
                        case "Unidade":
                            produtos = produtos.OrderBy(d => d.Unidade);
                            break;
                        case "Quantidade":
                            produtos = produtos.OrderBy(d => d.Quantidade);
                            break;
                        default:
                            break;
                    }
                }
            }

            List<Produto> result = produtos.ToList();
            return result;
        }

        public string[] BuscarAutores()
        {
            string[] result = _context.Usuarios.Select(u => u.Nome).ToArray();
            return result;
        }

        public string[] BuscarEquipes()
        {
            string[] result = _context.Equipes.Select(p => p.Nome).ToArray();
            return result;
        }

        public string[] BuscarProdutos()
        {
            string[] result = _context.Produtos.Select(p => p.Categoria).ToArray();
            return result;
        }

        public List<Equipe> BuscarPontos()
        {
            List<Equipe> result = _context.Equipes.ToList();
            return result;
        }

    }
}
