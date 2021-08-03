FROM nginx

COPY --from=gik25138.live.dynatrace.com/linux/oneagent-codemodules-musl:all / /
ENV LD_PRELOAD /opt/dynatrace/oneagent/agent/lib64/liboneagentproc.so
ENV DT_TENANT=gik25138.live.dynatrace.com
ENV DT_TENANTTOKEN=dt0c01.IHCMSCMBZ3XFKDFHARKIZPQQ.JXCPGNGHVSQQ2L6KLKS3HZCXF5TYR3QU7GVOBR2CJKNKXQNC6ZL4C43ODEE4OYA2

ARG NGINX_CONF
RUN echo $NGINX_CONF
ENV FILE="${NGINX_CONF}.nginx.conf"
COPY $FILE /etc/nginx/nginx.conf

